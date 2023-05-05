using AutoMapper;
using EPiServer;
using EPiServer.Core;
using EPiServer.Globalization;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Web.Features.Navigation.Models;
using Perficient.Web.Features.Navigation.Settings;
using Perficient.Web.Features.Navigation.ViewModels;
using System.Globalization;
using System.Linq;

namespace Perficient.Web.Features.Navigation.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ISettingsService _settingsService;
        private readonly IContentLoader _contentLoader;
        private readonly IMapper _mapper;

        public NavigationService(ISettingsService settingsService,
            IContentLoader contentLoader,
            IMapper mapper)
        {
            _settingsService = settingsService;
            _contentLoader = contentLoader;
            _mapper = mapper;
        }

        public FooterContainerViewModel GetFooter(CultureInfo preferredLanguage)
        {
            var footerContentArea = _settingsService.GetSiteSettings<NavigationSettings>(preferredLanguage)?.FooterContent;

            if (footerContentArea?.FilteredItems?.FirstOrDefault() == null)
            {
                return new FooterContainerViewModel();
            }

            var contentLoaderOption = new LoaderOptions().Add(LanguageLoaderOption.Fallback(preferredLanguage));
            var footerBlock = _contentLoader.Get<FooterBlock>(footerContentArea?.FilteredItems?.FirstOrDefault().ContentLink, contentLoaderOption);

            var footerContainer = GetFooter(footerBlock);
            footerContainer.Footer.Language = preferredLanguage.Name;

            return footerContainer;
        }

        public FooterContainerViewModel GetFooter(FooterBlock footerBlock)
        {
            if (footerBlock == null)
            {
                return new FooterContainerViewModel();
            }

            var footerViewModel = _mapper.Map<FooterViewModel>(footerBlock);
            footerViewModel.Language = ContentLanguage.PreferredCulture.Name;

            return new FooterContainerViewModel()
            {
                Footer = footerViewModel,
            };
        }

        public HeaderContainerViewModel GetHeader(CultureInfo preferredLanguage)
        {
            var headerContentArea = _settingsService.GetSiteSettings<NavigationSettings>(preferredLanguage)?.HeaderContent;

            if (headerContentArea?.FilteredItems?.FirstOrDefault() == null)
            {
                return new HeaderContainerViewModel();
            }

            var contentLoaderOption = new LoaderOptions().Add(LanguageLoaderOption.Fallback(preferredLanguage));
            var headerBlock = _contentLoader.Get<HeaderBlock>(headerContentArea?.FilteredItems?.FirstOrDefault().ContentLink, contentLoaderOption);

            var headerContainer = GetHeader(headerBlock);
            headerContainer.Header.Language = preferredLanguage.Name;

            return headerContainer;
        }

        public HeaderContainerViewModel GetHeader(HeaderBlock headerBlock)
        {
            if (headerBlock == null)
            {
                return new HeaderContainerViewModel();
            }

            var headerViewModel = _mapper.Map<HeaderViewModel>(headerBlock);
            headerViewModel.Language = ContentLanguage.PreferredCulture.Name;

            return new HeaderContainerViewModel()
            {
                Header = headerViewModel,
            };
        }
    }
}
