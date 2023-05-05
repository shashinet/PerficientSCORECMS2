using AutoMapper;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using Perficient.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Web.Middleware.ContentMapping
{
    public class ContentAreaMemberResolver : IMemberValueResolver<object, object, ContentArea, List<object>>
    {
        private readonly IContentLoader _contentLoader;
        private readonly IContentAreaLoader _contentAreaLoader;
        private readonly IContentMapper _contentMapper;

        public ContentAreaMemberResolver(IContentLoader contentLoader,
            IContentAreaLoader contentAreaLoader,
            IContentMapper contentMapper)
        {
            _contentLoader = contentLoader;
            _contentAreaLoader = contentAreaLoader;
            _contentMapper = contentMapper;
        }

        public List<object> Resolve(
            object source,
            object destination,
            ContentArea sourceMember,
            List<object> destMember,
            ResolutionContext context)
        {
            var returnValue = new List<object>();

            if (sourceMember == null || !sourceMember.FilteredItems.Any())
            {
                return returnValue;
            }

            foreach (var contentAreaItem in sourceMember.FilteredItems)
            {
                var contentItem = _contentLoader.Get<IContent>(contentAreaItem.ContentLink);
                var obj = _contentMapper.MapContentTypes(contentItem);

                if (obj == null)
                {
                    continue;
                }

                var displayOption = _contentAreaLoader.LoadDisplayOption(contentAreaItem);
                if (displayOption != null && !string.IsNullOrEmpty(displayOption.Tag))
                {
                    obj.TrySetProperty("DisplayOption", displayOption.Tag);
                }

                returnValue.Add(obj);
            }

            return returnValue;
        }
    }
}
