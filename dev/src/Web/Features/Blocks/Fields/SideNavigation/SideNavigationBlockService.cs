using AutoMapper;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;
using Perficient.Infrastructure.Attributes;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Enums;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Interfaces;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation
{
    public class SideNavigationBlockService : ISideNavigationBlockService
    {
        private readonly IContentRepository _contentRepo;
        private readonly IPageRouteHelper _pageRouteHelper;
        private readonly IMapper _mapper;

        public SideNavigationBlockService(IContentRepository contentRepo, IPageRouteHelper pageRouteHelper, IMapper mapper)
        {
            _contentRepo = contentRepo;
            _pageRouteHelper = pageRouteHelper;
            _mapper = mapper;
        }

        public SideNavigationReactViewModel CreateSecondaryNavigation(SideNavigationBlock currentBlock)
        {
            var model = CreateSideNavigation(currentBlock);
            var sideNavItems = _mapper.Map<List<SideNavigationItems>>(model.NavigationItems);
            var pageMainTitle = (currentBlock.NavigationType != SideNavigationType.CurrentContent) ? (_pageRouteHelper.Page.Name) : sideNavItems.FirstOrDefault()?.Title;
            var sideNavMenuItems = (currentBlock.NavigationType != SideNavigationType.CurrentContent) ? (sideNavItems) : sideNavItems.FirstOrDefault()?.ChildPages;

            var secViewModel = new SideNavigationReactViewModel()
            {
                Title = pageMainTitle,
                NavigationItems = sideNavMenuItems,
            };
            return secViewModel;
        }

        public SideNavigationViewModel CreateSideNavigation(SideNavigationBlock currentBlock)
        {
            var baseViewModel = new SideNavigationViewModel()
            {
                CssClasses = currentBlock.SideNavStyle,
                NavigationItems = new List<SideNavigationLinkViewModel>()
            };

            var parentAttribute = GetParentContentTypeAttribute();
            if (parentAttribute != null)
            {
                return CreateSideNavigationFromAttribute(baseViewModel, parentAttribute);
            }

            switch (currentBlock.NavigationType)
            {
                case SideNavigationType.ContentArea:
                    return CreateSideNavigationFromContentArea(baseViewModel, currentBlock);

                case SideNavigationType.TopLevelLinks:
                    return CreateSideNavigationFromTopLevelLinks(baseViewModel, currentBlock);

                case SideNavigationType.CurrentContent:
                    return CreateSideNavigationFromCurrentContent(baseViewModel, currentBlock);

                default:
                    break;
            }



            return baseViewModel;
        }

        #region Private Methods

        private SideNavigationViewModel CreateSideNavigationFromContentArea(
            SideNavigationViewModel viewModel,
            SideNavigationBlock currentBlock)
        {
            if (!currentBlock.NavigationItems?.FilteredItems?.Any() ?? true)
            {
                return viewModel;
            }

            viewModel.NavigationItems = BuildSideNavigationItemsFromContentArea(currentBlock.NavigationItems.FilteredItems);

            return viewModel;
        }

        private SideNavigationViewModel CreateSideNavigationFromTopLevelLinks(
            SideNavigationViewModel viewModel,
            SideNavigationBlock currentBlock)
        {
            var linkItems = currentBlock.TopLevelLinks?.Select(x => _contentRepo.Get<IContent>(x));

            if (!linkItems?.Any() ?? true)
            {
                return viewModel;
            }

            viewModel.NavigationItems = BuildSideNavigationItemsFromArchitecture(currentBlock.NavigationMaxDepth, linkItems, 1);

            return viewModel;
        }

        private SideNavigationViewModel CreateSideNavigationFromCurrentContent(
            SideNavigationViewModel viewModel,
            SideNavigationBlock currentBlock)
        {
            var menuRoot = GetMenuRoot(currentBlock);

            if (menuRoot == null)
            {
                return viewModel;
            }

            var linkItems = new List<IContent>() { menuRoot };
            viewModel.NavigationItems = BuildSideNavigationItemsFromArchitecture(currentBlock.NavigationMaxDepth, linkItems, 1);

            return viewModel;
        }

        private SideNavigationViewModel CreateSideNavigationFromAttribute(
            SideNavigationViewModel viewModel,
            ParentMenuContentTypeAttribute parentAttribute)
        {
            if (parentAttribute == null)
            {
                return viewModel;
            }

            var menuRoot = GetMenuRoot(parentAttribute);

            if (menuRoot == null)
            {
                return viewModel;
            }

            var linkitems = new List<IContent>() { menuRoot };
            viewModel.NavigationItems = BuildSideNavigationItemsFromArchitecture(parentAttribute.Depth, linkitems, 1);

            return viewModel;
        }

        private IContent GetMenuRoot(SideNavigationBlock currentBlock)
        {
            var currentPage = _pageRouteHelper.Page;
            if (currentBlock.MenuDirection?.Equals("Down") ?? true)
            {
                return currentPage;
            }

            IContent menuRoot = null;

            var ancestors = _contentRepo.GetAncestors(currentPage?.ContentLink);

            if (!ancestors?.Any() ?? true)
            {
                return menuRoot;
            }

            menuRoot = ancestors.Count() < currentBlock.NavigationMaxDepth
                ? ancestors.ElementAtOrDefault(ancestors.Count())
                : ancestors.ElementAtOrDefault(currentBlock.NavigationMaxDepth);

            return menuRoot;
        }

        private IContent GetMenuRoot(ParentMenuContentTypeAttribute parentAttribute)
        {
            var page = _pageRouteHelper.Page;

            var menuRoot = _contentRepo
               .GetAncestors(page?.ContentLink)
               .Where(x => x.GetOriginalType() == parentAttribute.ParentType)
               ?.OrderBy(x => x.ContentLink.ID)
               ?.LastOrDefault();

            return menuRoot;
        }

        private List<SideNavigationLinkViewModel> BuildSideNavigationItemsFromContentArea(IEnumerable<ContentAreaItem> filteredItems)
        {
            var navigationLinks = new List<SideNavigationLinkViewModel>();

            var linkItems = filteredItems.Select(x => _contentRepo.Get<SideNavigationLinkItem>(x.ContentLink));

            foreach (var linkItem in linkItems)
            {
                var itemViewModel = new SideNavigationLinkViewModel()
                {
                    Title = linkItem.NavTitle,
                    AriaLabel = linkItem.NavAriaText,
                    OpenInNewWindow = linkItem.OpenLinkInNewWindow,
                    Link = UrlResolver.Current.GetUrl(linkItem.NavItemLink.ToString())
                };

                if (linkItem.NavItemChildLinks?.FilteredItems?.Any() ?? false)
                {
                    itemViewModel.ChildLinks = BuildSideNavigationItemsFromContentArea(linkItem.NavItemChildLinks.FilteredItems);
                }

                navigationLinks.Add(itemViewModel);
            }

            return navigationLinks;
        }

        private List<SideNavigationLinkViewModel> BuildSideNavigationItemsFromArchitecture(
            int maxDepth,
            IEnumerable<IContent> linkItems,
            int currentDepth)
        {
            var navigationLinkViewModels = new List<SideNavigationLinkViewModel>();

            if (currentDepth > maxDepth)
            {
                return navigationLinkViewModels;
            }

            var selectedPage = _pageRouteHelper.Page.ContentLink;

            foreach (var link in linkItems)
            {
                var navLinkViewModel = new SideNavigationLinkViewModel()
                {
                    Title = link.Name,
                    AriaLabel = link.Name,
                    Link = UrlResolver.Current.GetUrl(link.ContentLink),
                    OpenInNewWindow = false,
                    IsActive = (link.ContentLink == selectedPage)
                };

                var linkChildren = _contentRepo
                    .GetChildren<IContent>(link.ContentLink)
                    .Where(x => x is not ContentFolder);

                if (linkChildren != null && linkChildren.Any())
                {
                    navLinkViewModel.ChildLinks = BuildSideNavigationItemsFromArchitecture(maxDepth, linkChildren, currentDepth + 1);
                }

                navigationLinkViewModels.Add(navLinkViewModel);
            }

            return navigationLinkViewModels;
        }

        private ParentMenuContentTypeAttribute GetParentContentTypeAttribute()
        {
            ParentMenuContentTypeAttribute parentAttribute = null;

            var pageProps = _pageRouteHelper.Page?.GetType()?.GetProperties();

            if (pageProps == null)
            {
                return parentAttribute;
            }

            foreach (var propInfo in pageProps)
            {
                var parent = propInfo.GetCustomAttribute(typeof(ParentMenuContentTypeAttribute));
                if (parent != null)
                {
                    parentAttribute = parent as ParentMenuContentTypeAttribute;
                    break;
                }
            }

            return parentAttribute;
        }

        #endregion
    }
}
