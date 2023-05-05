using Perficient.Web.Features.Navigation.Models;
using Perficient.Web.Features.Navigation.ViewModels;
using System.Globalization;

namespace Perficient.Web.Features.Navigation.Services
{
    public interface INavigationService
    {
        public FooterContainerViewModel GetFooter(CultureInfo preferredLanguage);

        public FooterContainerViewModel GetFooter(FooterBlock footerBlock);


        public HeaderContainerViewModel GetHeader(CultureInfo preferredLanguage);

        public HeaderContainerViewModel GetHeader(HeaderBlock headerBlock);
    }
}
