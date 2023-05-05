using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Extensions;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.Models.ViewModels;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Web.Features.Navigation.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Navigation.Controllers
{
    public class BreadcrumbBlockComponent : AsyncPartialContentComponent<BreadcrumbBlock>
    {
        private readonly IContentLoader _contentLoader;
        private readonly IPageRouteHelper _pageRouteHelper;
        private readonly ISettingsService _settingsService;
        private readonly IUrlResolver _urlResolver;

        public BreadcrumbBlockComponent(
            IContentLoader contentLoader,
            IPageRouteHelper pageRouteHelper,
            ISettingsService settingsService,
            IUrlResolver urlResolver)
        {
            _contentLoader = contentLoader;
            _pageRouteHelper = pageRouteHelper;
            _settingsService = settingsService;
            _urlResolver = urlResolver;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(BreadcrumbBlock currentBlock)
        {
            var breadcrumb = _settingsService.GetSiteSettings<Perficient.Infrastructure.Settings.Models.Content.SiteSettings>()?.BreadcrumbIcon;
            if (breadcrumb != null && breadcrumb != EPiServer.Core.ContentReference.EmptyReference)
            {
                currentBlock.HomeIcon = breadcrumb;
            }

            var currentContent = _pageRouteHelper.PageLink;

            var breadcrumbs = _contentLoader.GetAncestors(currentContent)
                .OfType<BasePage>()
                .FilterForDisplay(true, true)
                .Reverse()
                .Where(x => x.VisibleInMenu)
                .SkipWhile(x => ContentReference.IsNullOrEmpty(x.ParentLink))
                .Select(x =>
                {
                    var selectedPage = _contentLoader.Get<BasePage>(x.ContentGuid, LanguageSelector.AutoDetect(true));
                    return new BaseLinkViewModel()
                    {
                        IsHome = false,
                        LinkTitle = string.IsNullOrEmpty(selectedPage.NavigationTitle) ? selectedPage.Name : selectedPage.NavigationTitle,
                        Url = _urlResolver.GetUrl(selectedPage.PageLink)
                    };
                }
                )
                .ToList();

            var currentPage = _contentLoader.Get<BasePage>(currentContent, LanguageSelector.AutoDetect(true));
            breadcrumbs.Add(new BaseLinkViewModel()
            {
                IsHome = false,
                LinkTitle = string.IsNullOrEmpty(currentPage.NavigationTitle) ? currentPage.Name : currentPage.NavigationTitle,
                Url = _urlResolver.GetUrl(currentContent)
            });

            breadcrumbs[0].IsHome = true;
            currentBlock.Links = breadcrumbs;

            return await Task.FromResult(View("~/Features/Navigation/Views/BreadcrumbBlock.cshtml", currentBlock));
        }
    }
}
