using AutoMapper;
using EPiServer;
using EPiServer.Core;
using Perficient.Web.Features.Navigation.Models;
using Perficient.Web.Features.Navigation.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Web.Middleware.ContentMapping
{
    public class LowerFooterValueResolver : IValueResolver<FooterBlock, FooterViewModel, LowerFooterViewModel>
    {
        private readonly IContentLoader _contentLoader;
        private readonly IContentMapper _contentMapper;
        public LowerFooterValueResolver(IContentLoader contentLoader, IContentMapper contentMapper)
        {
            _contentLoader = contentLoader;
            _contentMapper = contentMapper;

        }

        public LowerFooterViewModel Resolve(FooterBlock source, FooterViewModel destination, LowerFooterViewModel destMember, ResolutionContext context)
        {
            var lowerFooter = new LowerFooterViewModel();

            if (source == null)
            {
                return null;
            }

            var socialIcons = new List<object>();
            if (source.UnderLogoContent?.FilteredItems.Any() != null)
            {
                foreach (var contentAreaItem in source.UnderLogoContent.FilteredItems)
                {
                    var contentItem = _contentLoader.Get<IContent>(contentAreaItem.ContentLink);
                    var obj = _contentMapper.MapContentTypes(contentItem);
                    socialIcons.Add(obj);
                }
                lowerFooter.SocialIcons = socialIcons;
            }

            var lowerContent = new List<object>();

            if (source.MainContentArea?.FilteredItems.Any() != null)
            {
                foreach (var contentAreaItem in source.MainContentArea.FilteredItems)
                {
                    var contentItem = _contentLoader.Get<IContent>(contentAreaItem.ContentLink);
                    var obj = _contentMapper.MapContentTypes(contentItem);
                    lowerContent.Add(obj);
                }
                lowerFooter.LowerFooterContent = lowerContent;
            }
            return lowerFooter;
        }
    }
}
