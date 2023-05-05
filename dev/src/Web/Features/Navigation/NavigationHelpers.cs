using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Web.Features.Navigation.Settings;

namespace Perficient.Web.Features.Navigation
{
    public static class NavigationHelpers
    {
        public static IHtmlContent RenderHeader<TModel>(this IHtmlHelper<TModel> helper)
        {
            var _settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var header = _settingsService.GetSiteSettings<NavigationSettings>()?.HeaderContent;
            if (header == null) { return HtmlString.Empty; }
            return helper.PropertyFor(m => header);
        }

        public static IHtmlContent RenderFooter<TModel>(this IHtmlHelper<TModel> helper)
        {
            var _settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var footer = _settingsService.GetSiteSettings<NavigationSettings>()?.FooterContent;
            if (footer == null) { return HtmlString.Empty; }
            return helper.PropertyFor(m => footer);
        }

        public static IHtmlContent RenderAlerts<TModel>(this IHtmlHelper<TModel> helper)
        {
            var _settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var alerts = _settingsService.GetSiteSettings<NavigationSettings>()?.AlertContent;
            if (alerts == null) { return HtmlString.Empty; }
            return helper.PropertyFor(m => alerts);
        }
    }
}
