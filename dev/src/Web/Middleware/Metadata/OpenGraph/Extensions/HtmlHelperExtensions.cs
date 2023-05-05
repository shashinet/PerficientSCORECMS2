using Boxed.AspNetCore.TagHelpers.OpenGraph;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Perficient.Infrastructure.Interfaces.ViewModels;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Middleware.Metadata.OpenGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Web.Middleware.Extensions
{
    public static class HtmlHelperExtensions
    {
        private static readonly Lazy<IContentTypeRepository> _contentTypeRepository = new Lazy<IContentTypeRepository>(() => ServiceLocator.Current.GetInstance<IContentTypeRepository>());
        private static readonly Lazy<IContentLanguageAccessor> _cultureAccessor = new Lazy<IContentLanguageAccessor>(() => ServiceLocator.Current.GetInstance<IContentLanguageAccessor>());

        public static IHtmlContent RenderOpenGraphMetaData(this IHtmlHelper helper, IContentViewModel<IContent> contentViewModel)
        {
            var metaTitle = (contentViewModel.CurrentContent as BasePage)?.MetaTitle ?? contentViewModel.CurrentContent.Name;
            var defaultLocale = _cultureAccessor.Value.Language;
            IEnumerable<string> alternateLocales = null;
            string contentType = null;
            string imageUrl = null;

            //TODO:  Add Responsive Image to create Page Image
            imageUrl = "http://ChangeMe.test";


            if (contentViewModel.CurrentContent is BasePage pageData)
            {
                alternateLocales = pageData.ExistingLanguages
                    .Where(culture => culture != defaultLocale)
                    .Select(culture => culture.TextInfo.CultureName.Replace('-', '_'));

                if (pageData.MetaContentType != null)
                {
                    contentType = pageData.MetaContentType;
                }
                else
                {
                    var pageType = _contentTypeRepository.Value.Load(contentViewModel.CurrentContent.GetOriginalType());
                    contentType = pageType.DisplayName;
                }
            }

            switch (contentViewModel.CurrentContent)
            {
                case BaseStartPage homePage:
                    var openGraphHomePage = new OpenGraphHomePage(metaTitle, new OpenGraphImage(new Uri(imageUrl)), GetUrl(homePage.ContentLink))
                    {
                        Description = homePage.PageDescription,
                        Locale = defaultLocale.Name.Replace('-', '_'),
                        AlternateLocales = alternateLocales,
                        ContentType = contentType,
                        ModifiedTime = homePage.Changed,
                        PublishedTime = homePage.StartPublish ?? null,
                        ExpirationTime = homePage.StopPublish ?? null
                    };

                    return helper.OpenGraph(openGraphHomePage);
                //case BlogDetailsPage blogDetailsPage:
                //    var openGraphBlogDetailsPage = new OpenGraphBlogDetailsPage(metaTitle, new OpenGraphImage(new Uri(imageUrl)), GetUrl(blogDetailsPage.ContentLink))
                //    {
                //        Description = blogDetailsPage.PageDescription,
                //        Locale = defaultLocale.Name.Replace('-', '_'),
                //        AlternateLocales = alternateLocales,
                //        Author = blogDetailsPage.Author,
                //        ContentType = contentType,
                //        ModifiedTime = blogDetailsPage.Changed,
                //        PublishedTime = blogDetailsPage.PublishedDate,
                //        ExpirationTime = blogDetailsPage.StopPublish ?? null
                //    };

                //    return helper.OpenGraph(openGraphBlogDetailsPage);

                case BasePage basePage:
                    var openGraphBasePage = new OpenGraphBasePage(metaTitle, new OpenGraphImage(new Uri(imageUrl)), GetUrl(basePage.ContentLink))
                    {
                        Description = basePage.PageDescription,
                        Locale = defaultLocale.Name.Replace('-', '_'),
                        AlternateLocales = alternateLocales,
                        Author = basePage.AuthorMetaData,
                        ContentType = contentType,
                        ModifiedTime = basePage.Changed,
                        PublishedTime = basePage.StartPublish ?? null,
                        ExpirationTime = basePage.StopPublish ?? null
                    };

                    return helper.OpenGraph(openGraphBasePage);                
            }

            return new HtmlString(string.Empty);
        }

        private static string GetUrl(ContentReference content)
        {
            var siteUrl = SiteDefinition.Current.SiteUrl;
            var url = new Uri(siteUrl, UrlResolver.Current.GetUrl(content));

            return url.ToString();
        }
    }
}
