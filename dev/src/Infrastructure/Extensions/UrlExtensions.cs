using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Perficient.Infrastructure.Models.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Perficient.Infrastructure.Extensions
{
    public static class UrlExtensions
    {
        private static readonly Lazy<ISiteDefinitionResolver> _siteDefinitionResolver =
            new Lazy<ISiteDefinitionResolver>(() => ServiceLocator.Current.GetInstance<ISiteDefinitionResolver>());

        private static readonly Lazy<IUrlResolver> _urlResolver =
            new Lazy<IUrlResolver>(() => ServiceLocator.Current.GetInstance<IUrlResolver>());

        private static readonly Lazy<IContentLoader> _contentLoader =
            new Lazy<IContentLoader>(() => ServiceLocator.Current.GetInstance<IContentLoader>());

        private static readonly Lazy<IContentRouteHelper> _pageRouteHelper =
            new Lazy<IContentRouteHelper>(() => ServiceLocator.Current.GetInstance<IContentRouteHelper>());

        public static string BuildUrl(this UrlHelper urlHelper, Url url)
        {
            if (url == null)
            {
                return string.Empty;
            }

            if (string.Equals(url.Scheme, "tel", StringComparison.InvariantCultureIgnoreCase) || string.Equals(url.Scheme, "mailto", StringComparison.InvariantCultureIgnoreCase))
            {
                return url.OriginalString;
            }

            return urlHelper.ContentUrl(url.Path);
        }

        public static string ExternalUrl(
            this ContentReference contentLink,
            CultureInfo contentLanguage)
        {
            VirtualPathArguments arguments = new VirtualPathArguments
            {
                ContextMode = ContextMode.Default,
                ForceCanonical = true
            };

            string resultUrl = _urlResolver.Value.GetUrl(contentLink, contentLanguage.Name, arguments);
            SiteDefinition siteDefinition = _siteDefinitionResolver.Value.GetByContent(contentLink, true, true);

            return resultUrl.ExternalUrl(siteDefinition, contentLanguage);
        }

        public static string ExternalUrl(
            this string url,
            SiteDefinition siteDefinition,
            CultureInfo contentLanguage)
        {
            // HACK: Temprorary fix until GetUrl and ForceCanonical works as expected,
            // i.e returning an absolute URL even if there is a HTTP context that matches the content's site definition and host.
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri relativeUri) || relativeUri.IsAbsoluteUri)
            {
                return url;
            }

            IEnumerable<HostDefinition> hosts = siteDefinition.GetHosts(contentLanguage, true);
            HostDefinition host = hosts.FirstOrDefault(h => h.Type == HostDefinitionType.Primary) ?? hosts.FirstOrDefault(h => h.Type == HostDefinitionType.Undefined);
            Uri baseUri = siteDefinition.SiteUrl ?? SiteDefinition.Current.SiteUrl;

            if (host != null && !host.Name.Equals("*"))
            {
                // Try to create a new base URI from the host with the site's URI scheme. Name should be a valid
                // authority, i.e. have a port number if it differs from the URI scheme's default port number.
                Uri.TryCreate(siteDefinition.SiteUrl.Scheme + "://" + host.Name, UriKind.Absolute, out baseUri);
            }

            Uri absoluteUri = new Uri(baseUri, relativeUri);
            return absoluteUri.AbsoluteUri;
        }

        public static HtmlString CustomCanonicalLink(this IHtmlHelper html)
        {
            if (_pageRouteHelper.Value.Content is not BasePage basePage)
            {
                return new HtmlString(html.CanonicalLink().ToString());
            }
            else if (basePage.LinkType == PageShortcutType.FetchData)
            {
                return GetShortcutPageCanonicalLink(html, basePage);
            }
            else if ((!string.IsNullOrWhiteSpace(basePage.CanonicalUrlOverride)) && basePage.LinkType == PageShortcutType.Normal)
            {
                return BuildCanonicalLinkNode(basePage, basePage.CanonicalUrlOverride);
            }
            else if (basePage.LinkType == PageShortcutType.Normal)
            {
                return GetCanonical(basePage);
            }

            return new HtmlString(string.Empty);
        }

        private static HtmlString BuildCanonicalLinkNode(BasePage basePage, string url)
        {
            var absoluteUrl = url.ExternalUrl(SiteDefinition.Current, basePage.Language);
            return new HtmlString($"<link rel=\"canonical\" href=\"{absoluteUrl}\"/>");
        }

        private static HtmlString GetCanonical(BasePage originalPage)
        {
            var externalUrl = originalPage.ContentLink.ExternalUrl(originalPage.Language);
            return BuildCanonicalLinkNode(originalPage, externalUrl);
        }

        private static HtmlString GetShortcutPageCanonicalLink(IHtmlHelper html, BasePage originalPage)
        {
            PageReference shortcutPage = originalPage.Property["PageShortcutLink"].Value as PageReference;

            if (shortcutPage == null || !_contentLoader.Value.TryGet(shortcutPage, out BasePage shortcutPageData))
            {
                return new HtmlString(html.CanonicalLink().ToString());
            }
            else if (!string.IsNullOrWhiteSpace(shortcutPageData.CanonicalUrlOverride))
            {
                return BuildCanonicalLinkNode(originalPage, shortcutPageData.CanonicalUrlOverride);
            }

            return GetCanonical(originalPage);
        }
    }
}
