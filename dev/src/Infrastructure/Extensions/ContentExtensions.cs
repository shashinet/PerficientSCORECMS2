using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Framework.Web;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.Extensions
{
    public static class ContentExtensions
    {
        private static readonly Injected<IContentLoader> _contentLoader = default;

        public static IEnumerable<PageData> GetSiblings(this PageData pageData) => GetSiblings(pageData, _contentLoader.Service);

        public static IEnumerable<PageData> GetSiblings(this PageData pageData, IContentLoader contentLoader)
        {
            var filter = new FilterContentForVisitor();
            return contentLoader.GetChildren<PageData>(pageData.ParentLink).Where(page => !filter.ShouldFilter(page));
        }

        public static IEnumerable<T> FilterForDisplay<T>(this IEnumerable<T> contents,
                                                         bool requirePageTemplate = false,
                                                         bool requireVisibleInMenu = false) where T : IContent
        {
            var accessFilter = new FilterAccess();
            var publishedFilter = new FilterPublished();
            contents = contents.Where(x => !publishedFilter.ShouldFilter(x) && !accessFilter.ShouldFilter(x));

            if (requirePageTemplate)
            {
                var templateFilter = ServiceLocator.Current.GetInstance<FilterTemplate>();
                templateFilter.TemplateTypeCategories = TemplateTypeCategories.Request;
                contents = contents.Where(x => !templateFilter.ShouldFilter(x));
            }

            if (requireVisibleInMenu)
            {
                contents = contents.Where(x => VisibleInMenu(x));
            }

            return contents;
        }

        private static bool VisibleInMenu(IContent content) => content is not PageData page || page.VisibleInMenu;

        public static IEnumerable<T> GetContentItems<T>(this IEnumerable<ContentAreaItem> contentAreaItems,
                                                        LanguageLoaderOption languageLoaderOption = null,
                                                        IContentLoader contentLoader = null) where T : IContentData
        {
            if (contentAreaItems == null)
            {
                return Enumerable.Empty<T>();
            }

            contentLoader ??= ServiceLocator.Current.GetInstance<IContentLoader>();
            languageLoaderOption ??= LanguageLoaderOption.FallbackWithMaster();

            return contentLoader.GetItems(contentAreaItems.Select(i => i.ContentLink), new LoaderOptions { languageLoaderOption }).OfType<T>();
        }

        public static IEnumerable<T> GetContentItems<T>(this IList<ContentReference> contentReferenceItems,
                                                        LanguageLoaderOption languageLoaderOption = null,
                                                        IContentLoader contentLoader = null) where T : IContentData
        {
            if (contentReferenceItems == null)
            {
                return Enumerable.Empty<T>();
            }

            contentLoader ??= ServiceLocator.Current.GetInstance<IContentLoader>();
            languageLoaderOption ??= LanguageLoaderOption.FallbackWithMaster();

            return contentLoader.GetItems(contentReferenceItems, new LoaderOptions { languageLoaderOption }).OfType<T>();
        }

        public static List<ContentReference> GetContentAncestors(this ContentReference content, ContentReference root = null)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

            var pageList = contentLoader.GetAncestors(content).Reverse();
            pageList = root != null
                ? pageList.SkipWhile(c => !c.ContentLink.CompareToIgnoreWorkID(root))
                : pageList.SkipWhile(c => ContentReference.IsNullOrEmpty(c.ParentLink));

            return pageList.Select(c => c.ContentLink).ToList();
        }

        public static List<T> GetContentAncestors<T>(this ContentReference content, ContentReference root = null) where T : class
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

            var pageList = contentLoader.GetAncestors(content);
            pageList = root != null
                ? pageList.SkipWhile(c => !c.ContentLink.CompareToIgnoreWorkID(root))
                : pageList.SkipWhile(c => ContentReference.IsNullOrEmpty(c.ParentLink));

            return pageList.OfType<T>().ToList();
        }

        public static ContentReference ContentLink(this BlockData currentContent)
        {
            return (currentContent as IContent)?.ContentLink;
        }

        public static bool TryGetBlockContent(this BlockData currentContent, out IContent blockContent)
        {
            blockContent = currentContent as IContent;

            return blockContent != null;
        }
    }
}
