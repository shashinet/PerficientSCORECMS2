using AutoMapper;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;
using Perficient.Web.Features.Blocks.Components.ResponsiveImage;
using Perficient.Web.Features.Blocks.Fields.ResponsivePicture;
using Perficient.Web.Features.Media;
using Perficient.Web.Features.Media.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Middleware.ContentMapping
{
    public class ImageMediaResolver : IMemberValueResolver<object, object, ContentReference, object>
    {
        private readonly IContentRepository _contentRepository;
        private readonly UrlResolver _urlResolver;
        private readonly IMapper _mapper;

        public ImageMediaResolver(IContentRepository cotentRepository, UrlResolver urlResolver, IMapper mapper)
        {
            _contentRepository = cotentRepository;
            _urlResolver = urlResolver;
            _mapper = mapper;
        }

        public object Resolve(object source, object destination, ContentReference sourceMember, object destMember, ResolutionContext context)
        {
            if (sourceMember == null)
            {
                return null;
            }

            var media = _contentRepository.Get<IContent>(sourceMember);

            switch (media)
            {
                case SvgMedia svg:
                    return new SvgMediaViewModel(svg.XML, svg.GetFriendlyAltText());
                case ImageMediaData image:
                    return new ImageMediaViewModel(_urlResolver.GetUrl(sourceMember), image.GetFriendlyAltText());
                case ResponsiveImageBlock responsiveImage:
                    var originalImage = _contentRepository.Get<ImageMediaData>(responsiveImage.ResponsiveImage.OriginalImage);

                    var viewModel = new ResponsiveImageViewModel();
                    var croppings = new List<object>();

                    foreach (PictureCropping cropping in responsiveImage?.ResponsiveImage?.Croppings)
                    {
                        croppings.Add(_mapper.Map<CroppedImageViewModel>(cropping));
                    }

                    viewModel.Croppings = croppings;
                    viewModel.Original = new ImageMediaViewModel(_urlResolver.GetUrl(responsiveImage?.ResponsiveImage?.OriginalImage), originalImage.GetFriendlyAltText());

                    return viewModel;
                default:
                    return null;
            }
        }
    }
}
