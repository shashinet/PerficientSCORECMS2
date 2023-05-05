using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using erficient.Infrastructure.Settings.Models.Content;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.Models.Properties;
using Perficient.Infrastructure.Models.ViewModels;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perficient.Infrastructure.Extensions
{
    public static class HtmlHelpers
    {
        private const string _cssFormat = "<link href=\"{0}\" rel=\"stylesheet\" />";
        private const string _scriptFormat = "<script src=\"{0}\"></script>";
        private const string _metaFormat = "<meta name=\"{0}\" property=\"{0}\" content=\"{1}\" />";

        private static readonly Lazy<IContentLoader> _contentLoader =
            new Lazy<IContentLoader>(() => ServiceLocator.Current.GetInstance<IContentLoader>());

        private static readonly Lazy<IContextModeResolver> _contextModeResolver =
           new Lazy<IContextModeResolver>(() => ServiceLocator.Current.GetInstance<IContextModeResolver>());

        private static readonly Lazy<IPermanentLinkMapper> _permanentLinkMapper =
           new Lazy<IPermanentLinkMapper>(() => ServiceLocator.Current.GetInstance<IPermanentLinkMapper>());

        private static readonly Lazy<ISettingsService> _settingsService =
            new Lazy<ISettingsService>(() => ServiceLocator.Current.GetInstance<ISettingsService>());

        private static readonly Lazy<IUrlResolver> _urlResolver =
           new Lazy<IUrlResolver>(() => ServiceLocator.Current.GetInstance<IUrlResolver>());

        public static bool IsInEditMode(this IHtmlHelper htmlHelper) => _contextModeResolver.Value.CurrentMode == ContextMode.Edit;


        public static void RenderContent(this IHtmlHelper helper, ContentReference contentRef, string templateTag = "")
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            if (contentLoader.TryGet(contentRef, out IContentData contentData))
            {
                helper.RenderContentData(contentData, false, templateTag);
            }
        }

        /// <summary>
        /// Renders an HTML container in which a React component will be mounted
        /// </summary>
        /// <param name="html">Associated HTML helper</param>
        /// <param name="component">Class or function name of React component</param>
        /// <param name="props">Optional object to pass as props to the component</param>
        /// <param name="tag">Optional string to pass as tag for the component wrapper</param>
        /// <param name="wrappingClassname">Optional string to pass as class for the component wrapper</param>
        /// <remarks>This HTML helper also ensure required script resources are loaded</remarks>
        /// <returns></returns>
        public static HtmlString ReactComponent(this IHtmlHelper html, string component, object props = null, string tag = "div", string wrappingClassname = null)
        {
            var propsJson = JsonConvert.SerializeObject(props, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), StringEscapeHandling = StringEscapeHandling.EscapeHtml });

            // Render container element for the React component (for more attributes, see http://world.episerver.com/documentation/developer-guides/CMS/editing/)
            return new HtmlString($"<{tag} data-react-component=\"{component}\" data-props='{propsJson ?? "{ }"}' class='{wrappingClassname}'></{tag}>");
        }

        /// <summary>
        /// Renders an HTML container in which a React component will be mounted associated with a content property
        /// </summary>
        /// <param name="html">Associated HTML helper</param>
        /// <param name="component">Class or function name of React component</param>
        /// <param name="props">Optional object to pass as props to the component</param>
        /// <param name="tag">Optional string to pass as surrounding html tag</param>
        /// <param name="propertyName">Name of the associated content property</param>
        /// <remarks>This HTML helper also ensure required script resources are loaded</remarks>
        /// <returns></returns>
        public static HtmlString ReactComponentFor(this IHtmlHelper html, string propertyName, string component, object props = null, string tag = "div")
        {
            var propsJson = JsonConvert.SerializeObject(props, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), StringEscapeHandling = StringEscapeHandling.EscapeHtml });

            // Ensure scripts for React components is included on the page
            ClientResources.RequireScript("", "ReactComponent", new[] { "react" });

            // Render container element for the React component (for more attributes, see http://world.episerver.com/documentation/developer-guides/CMS/editing/)
            return html.IsInEditMode() ?
                    new HtmlString($"<{tag} data-epi-property-name=\"{propertyName}\" data-epi-property-render=\"none\" data-react-component=\"{component}\" data-props='{propsJson ?? "{ }"}'></{tag}>") :
                    html.ReactComponent(component, props, tag);
        }

        public static HtmlString RenderJsonEditMode(this IHtmlHelper html, string id, object viewModel = null)
        {
            var serializedJson = JsonConvert.SerializeObject(viewModel, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), StringEscapeHandling = StringEscapeHandling.EscapeHtml });

            var output = new StringBuilder(string.Empty);
            output.AppendLine("<pre id=\"json-" + id + "\" style=\"overflow-x:scroll;background:#ddd;\">");
            output.AppendLine(serializedJson);
            output.AppendLine("</pre>");

            return html.IsInEditMode() ?
                new HtmlString(output.ToString()) :
                new HtmlString("");
        }

        public static HtmlString RenderMetaData(this IHtmlHelper htmlHelper, IContent content)
        {
            if (content == null)
            {
                return new HtmlString("");
            }

            var output = new StringBuilder(string.Empty);
            if (!(content is BasePage sitePageData))
            {
                return new HtmlString("");
            }

            if (!string.IsNullOrWhiteSpace(sitePageData.MetaTitle))
            {
                output.AppendLine(string.Format(_metaFormat, "title", sitePageData.MetaTitle));
            }

            if (!string.IsNullOrEmpty(sitePageData.Keywords))
            {
                output.AppendLine(string.Format(_metaFormat, "keywords", sitePageData.Keywords));
            }

            if (!string.IsNullOrWhiteSpace(sitePageData.PageDescription))
            {
                output.AppendLine(string.Format(_metaFormat, "description", sitePageData.PageDescription));
            }

            if (sitePageData.DisableIndexing)
            {
                output.AppendLine("<meta name=\"robots\" content=\"NOINDEX, NOFOLLOW\">");
            }

            return new HtmlString(output.ToString());
        }

        public static List<BaseLinkViewModel> GetBreadcrumbs(this IHtmlHelper helper)
        {

            var routeHelper = ServiceLocator.Current.GetInstance<IPageRouteHelper>();
            var currentContent = routeHelper.PageLink;

            var breadcrumbs = _contentLoader.Value.GetAncestors(currentContent)
                .OfType<BasePage>()
                .FilterForDisplay(true, true)
                .Reverse()
                .Where(x => x.VisibleInMenu)
                .SkipWhile(x => ContentReference.IsNullOrEmpty(x.ParentLink))
                .Select(x =>
                {
                    var selectedPage = _contentLoader.Value.Get<BasePage>(x.ContentGuid, LanguageSelector.AutoDetect(true));
                    return new BaseLinkViewModel()
                    {
                        LinkTitle = string.IsNullOrEmpty(selectedPage.NavigationTitle) ? selectedPage.Name : selectedPage.NavigationTitle,
                        Url = _urlResolver.Value.GetUrl(selectedPage.PageLink)
                    };
                }
                )
                .ToList();

            var currentPage = _contentLoader.Value.Get<BasePage>(currentContent, LanguageSelector.AutoDetect(true));
            breadcrumbs.Add(new BaseLinkViewModel()
            {
                LinkTitle = string.IsNullOrEmpty(currentPage.NavigationTitle) ? currentPage.Name : currentPage.NavigationTitle,
                Url = _urlResolver.Value.GetUrl(currentContent)
            });
            return breadcrumbs;
        }


        public static HtmlString RenderHeaderScripts(this IHtmlHelper helper, IContent content)
        {
            if (content == null
                || ContentReference.StartPage == PageReference.EmptyReference
                || content is not BasePage)
            {
                return new HtmlString("");
            }

            // Injection Hierarchically Javascript
            var settings = _settingsService.Value.GetSiteSettings<ScriptInjectionSettings>();

            if (settings == null || settings.HeaderScripts == null)
            {
                return new HtmlString(string.Empty);
            }

            var outputScript = ProcessScripts(settings.HeaderScripts, content);

            return new HtmlString(outputScript);
        }

        public static HtmlString RenderFooterScripts(this IHtmlHelper helper, IContent content)
        {
            if (content == null
                || ContentReference.StartPage == PageReference.EmptyReference
                || content is not BasePage)
            {
                return new HtmlString("");
            }

            // Injection Hierarchically Javascript
            var settings = _settingsService.Value.GetSiteSettings<ScriptInjectionSettings>();

            if (settings == null || settings.FooterScripts == null)
            {
                return new HtmlString(string.Empty);
            }

            var outputScript = ProcessScripts(settings.FooterScripts, content);

            return new HtmlString(outputScript);
        }

        public static HtmlString RenderExtendedCss(this IHtmlHelper helper, IContent content)
        {
            if (content == null || ContentReference.StartPage == PageReference.EmptyReference || !(content is BasePage sitePageData))
            {
                return new HtmlString("");
            }

            var outputCss = new StringBuilder(string.Empty);
            var startPage = _contentLoader.Value.Get<BaseStartPage>(ContentReference.StartPage);

            // Extended Css file
            AppendFiles(startPage.CssFiles, outputCss, _cssFormat);
            if (!(sitePageData is BaseStartPage))
            {
                AppendFiles(sitePageData.CssFiles, outputCss, _cssFormat);
            }

            // Inline CSS
            if (!string.IsNullOrWhiteSpace(startPage.Css) || !string.IsNullOrWhiteSpace(sitePageData.Css))
            {
                outputCss.AppendLine("<style>");
                outputCss.AppendLine(!string.IsNullOrWhiteSpace(startPage.Css) ? startPage.Css : "");
                outputCss.AppendLine(!string.IsNullOrWhiteSpace(sitePageData.Css) && !(sitePageData is BaseStartPage) ? sitePageData.Css : "");
                outputCss.AppendLine("</style>");
            }

            return new HtmlString(outputCss.ToString());
        }

        private static void AppendFiles(IList<ContentReference> files, StringBuilder outputString, string formatString)
        {
            if (files == null || files.Count <= 0)
            {
                return;
            }

            foreach (var item in files.Where(item => !string.IsNullOrEmpty(_urlResolver.Value.GetUrl(item))))
            {
                var url = _urlResolver.Value.GetUrl(item);
                outputString.AppendLine(string.Format(formatString, url));
            }
        }

        private static void AppendFiles(LinkItemCollection files, StringBuilder outputString, string formatString)
        {
            if (files == null || files.Count <= 0) return;

            foreach (var item in files.Where(item => !string.IsNullOrEmpty(item.Href)))
            {
                var map = _permanentLinkMapper.Value.Find(new UrlBuilder(item.Href));
                outputString.AppendLine(map == null
                    ? string.Format(formatString, item.GetMappedHref())
                    : string.Format(formatString, _urlResolver.Value.GetUrl(map.ContentReference)));
            }
        }

        public static HtmlString RenderGoogleTagManagerHead(this IHtmlHelper htmlHelper)
        {
            var googleTagManagerKey = _settingsService.Value.GetSiteSettings<SiteSettings>()?.GtmKey;
            if (string.IsNullOrEmpty(googleTagManagerKey))
            {
                return new HtmlString("");
            }

            var output = new StringBuilder(string.Empty);
            output.AppendLine("<!--Google Tag Manager -->");
            output.AppendLine("<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':");
            output.AppendLine("new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],");
            output.AppendLine("j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=");
            output.AppendLine("'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);");
            output.AppendLine($"}})(window,document,'script','dataLayer','{googleTagManagerKey}');</script>");
            output.AppendLine("<!-- End Google Tag Manager -->");
            return new HtmlString(output.ToString());
        }

        public static HtmlString RenderGoogleTagManagerNoScript(this IHtmlHelper htmlHelper)
        {
            var googleTagManagerKey = _settingsService.Value.GetSiteSettings<SiteSettings>()?.GtmKey;
            if (string.IsNullOrEmpty(googleTagManagerKey))
            {
                return new HtmlString("");
            }

            var output = new StringBuilder(string.Empty);

            output.AppendLine("<!-- Google Tag Manager (noscript) -->");
            output.AppendLine("<noscript>");
            output.AppendLine($"    <iframe src=\"https://www.googletagmanager.com/ns.html?id={googleTagManagerKey}\" height=\"0\" width=\"0\" style=\"display: none; visibility: hidden\" ></ iframe > ");
            output.AppendLine("</noscript>");
            output.AppendLine("<!-- End Google Tag Manager (noscript) -->");

            return new HtmlString(output.ToString());
        }

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

        private static string ProcessScripts(IList<ScriptInjectionModel> scripts, IContent content)
        {
            var outputScript = new StringBuilder(string.Empty);

            foreach (var script in scripts)
            {
                var pages = _contentLoader.Value.GetDescendents(script.ScriptRoot);

                if (!pages.Any(x => x == content.ContentLink) && content.ContentLink != script.ScriptRoot)
                {
                    continue;
                }

                // Script Files
                AppendFiles(script.ScriptFiles, outputScript, _scriptFormat);

                // External Javascript
                if (!string.IsNullOrWhiteSpace(script.ExternalScripts))
                {
                    outputScript.AppendLine(script.ExternalScripts);
                }

                // Inline Javascript
                if (!string.IsNullOrWhiteSpace(script.InlineScripts))
                {
                    outputScript.AppendLine("<script type=\"text/javascript\">");
                    outputScript.AppendLine(script.InlineScripts);
                    outputScript.AppendLine("</script>");
                }
            }

            return outputScript.ToString();
        }
    }
}
