using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.Maui.Graphics;

namespace Perficient.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinImageSizeAttribute : ValidationAttribute
    {
        private readonly int _minHeight;
        private readonly int _minWidth;
        protected readonly Injected<IContentRepository> _contentRepository;
        protected readonly Injected<IImageLoadingService> _imageLoadingService;

        public MinImageSizeAttribute(
            int minHeight, int minWidth)
        {
            _minHeight = minHeight;
            _minWidth = minWidth;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var refImage = value as ContentReference;

            if (refImage != null)
            {
                ImageData asset = _contentRepository.Service.Get<ImageData>(refImage);
               
                using IImage image1 = _imageLoadingService.Service.FromStream(asset.BinaryData.OpenRead());
                var height = image1.Height;
                var width = image1.Width;

                if ((height < _minHeight) && (width < _minWidth))
                {
                    return new ValidationResult($"Image minimum height should be {_minHeight} pixels and minimum width shoud be {_minWidth} pixels");
                }
                else if (height < _minHeight)
                {
                    return new ValidationResult($"Image minimum height should be {_minHeight} pixels");
                }
                else if (width < _minWidth)
                {
                    return new ValidationResult($"Image minimum width should be {_minWidth} pixels");
                }
            }
            return null;

        }
    }
}
