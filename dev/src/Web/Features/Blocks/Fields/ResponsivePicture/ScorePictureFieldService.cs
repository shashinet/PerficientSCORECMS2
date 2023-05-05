using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Framework.Blobs;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using Perficient.Web.Features.Media;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Image = SixLabors.ImageSharp.Image;
using ImageData = EPiServer.Core.ImageData;
using Rectangle = SixLabors.ImageSharp.Rectangle;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    public class ScorePictureFieldService : IScorePictureFieldService
    {
        protected readonly Injected<IContentRepository> _contentRepository;
        protected readonly Injected<IContentTypeRepository> _contentTypeRepository;
        protected readonly Injected<IBlobFactory> _blobFactory;

        public ScorePictureFieldService()
        {
        }

        public ImageData Crop(CropperPost cropResult)
        {
            var originalImage = GetImage(cropResult.epiData.contentLink);
            var croppingFolder = GetCroppingFolder(new ContentReference(cropResult.mainImage));
            var croppedImageData = MakeImage(originalImage, cropResult, cropResult.deviceSpec);

            if (originalImage.ContentLink.ID != cropResult.mainImage)
            {
                originalImage = GetImage(cropResult.mainImage);
            }

            var fileName = GenerateImageFileName(originalImage.Name,
                cropResult?.deviceSpec?.device,
                cropResult?.baseBlockData?.BlockId);

            var files = _contentRepository.Service.GetChildren<ImageMediaData>(croppingFolder.ContentLink);
            var imageFile = files.FirstOrDefault(img => img.Name == fileName);

            Blob blob;
            if (imageFile != null) // image exists and we need to overwrite it
            {
                var imageFileWritableClone = imageFile.CreateWritableClone() as ImageMediaData;
                blob = _blobFactory.Service.CreateBlob(imageFile.BinaryDataContainer, Path.GetExtension(originalImage.Name));
                blob.WriteAllBytes(croppedImageData);

                imageFileWritableClone.BinaryData = blob;
                imageFileWritableClone.Name = fileName;
                _contentRepository.Service.Save(imageFileWritableClone, SaveAction.Publish, AccessLevel.NoAccess);

                return imageFileWritableClone;
            }

            var newImage = _contentRepository.Service.GetDefault<ImageMediaData>(croppingFolder.ContentLink);
            blob = _blobFactory.Service.CreateBlob(newImage.BinaryDataContainer, Path.GetExtension(originalImage.Name));
            blob.WriteAllBytes(croppedImageData);

            newImage.BinaryData = blob;
            newImage.Name = fileName;
            _contentRepository.Service.Save(newImage, SaveAction.Publish, AccessLevel.NoAccess);

            return newImage;
        }

        public SystemCroppingFolder GetCroppingFolder(ContentReference parentReference)
        {
            var croppingRoot = _contentRepository.Service.GetChildren<SystemCroppingFolder>(parentReference).FirstOrDefault(x => !x.IsDeleted);

            if (croppingRoot == null)
            {
                croppingRoot = _contentRepository.Service.GetDefault<SystemCroppingFolder>(parentReference);
                croppingRoot.Name = "_croppings";
                _contentRepository.Service.Save(croppingRoot, SaveAction.Publish, AccessLevel.NoAccess);
            }

            return croppingRoot;
        }

        public List<CropResult> CroppingsExistFor(CheckImage checkImage)
        {
            var retVal = new List<CropResult>();

            if (string.IsNullOrEmpty(checkImage?.BlockData?.BlockId))
            {
                return retVal;
            }

            var croppingFolder = GetCroppingFolder(new ContentReference(checkImage.ImageId, true));

            var files = _contentRepository.Service.GetChildren<ImageMediaData>(croppingFolder.ContentLink).ToList();

            if (!files.Any())
            {
                return retVal;
            }

            foreach (var device in checkImage.Devices)
            {
                var name = GenerateImageFileName(checkImage.ImageName, device, checkImage.BlockData.BlockId);

                var image = files.FirstOrDefault(x => x.Name.Equals(name));

                if (image != null)
                {
                    retVal.Add(new CropResult
                    {
                        Image = image.ContentLink,
                        Device = device
                    });
                }
            }

            return retVal;
        }

        public string GenerateImageFileName(string originalImageName, string deviceName, string blockId)
        {
            var parentData = _contentRepository.Service.Get<IContent>(new ContentReference(blockId));
            var parentType = _contentTypeRepository.Service.Load(parentData.ContentTypeID).DisplayName ?? _contentTypeRepository.Service.Load(parentData.ContentTypeID).FullName;

            return $"{parentType}-{deviceName}{Path.GetExtension(originalImageName)}";
        }

        private ImageData GetImage(int mediaId)
        {
            return _contentRepository.Service.Get<ImageData>(new ContentReference(mediaId));
        }

        private byte[] MakeImage(IContentMedia media, CropperPost regionToCrop, DeviceSpec viewport)
        {
            using (var outStream = new MemoryStream())
            using (var image = Image.Load(media.BinaryData.OpenRead()))
            {
                image.Mutate(i => i
                    .Crop(new Rectangle(
                        (int)Math.Round(regionToCrop.data.x),
                        (int)Math.Round(regionToCrop.data.y),
                        (int)Math.Round(regionToCrop.data.width),
                        (int)Math.Round(regionToCrop.data.height)))
                    .Resize(viewport.width, viewport.height)
                );

                image.Save(outStream, new JpegEncoder { Quality = 80 });

                return outStream.ToArray();
            }
        }

        protected virtual byte[] Crop(byte[] imageData, IImageFormat imageFormat, CropperPost regionToCrop, DeviceSpec viewport)
        {
            using (var outStream = new MemoryStream())
            using (var image = Image.Load(imageData))
            {
                image.Mutate(i => i.Crop(new Rectangle(
                    (int)Math.Round(regionToCrop.data.x),
                    (int)Math.Round(regionToCrop.data.y),
                    (int)Math.Round(regionToCrop.data.width),
                    (int)Math.Round(regionToCrop.data.height))));

                image.Save(outStream, new JpegEncoder { Quality = 80 });

                return outStream.ToArray();
            }
        }
    }
}
