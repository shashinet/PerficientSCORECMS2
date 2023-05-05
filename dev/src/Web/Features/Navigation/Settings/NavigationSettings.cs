using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Settings.Abstracts;
using Perficient.Infrastructure.Settings.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Settings
{

    [SettingsContentType(DisplayName = "Navigation Settings",
        GUID = "6b84e0b3-14b7-4aba-8089-4b18aad54913",
        Description = "Header settings, footer settings, menu settings",
        AvailableInEditMode = true,
        SettingsName = "Navigation Settings")]
    [ImageUrl("~/icons/cms/pages/cms-icon-page-layout-settings.png")]
    public class NavigationSettings : SettingsBase
    {
        [Display(Name = "Header Content",
            GroupName = SystemTabNames.Content,
            Order = 10,
            Description = "This various blocks that comprise the website's header")]
        [CultureSpecific]
        [AllowedTypes(typeof(IHeaderBlock))]
        public virtual ContentArea HeaderContent { get; set; }

        [Display(Name = "Footer Content",
           GroupName = SystemTabNames.Content,
           Order = 10,
           Description = "This various blocks that comprise the website's footer")]
        [CultureSpecific]
        [AllowedTypes(typeof(IFooterBlock))]
        public virtual ContentArea FooterContent { get; set; }

        [Display(Name = "Alert Content",
           GroupName = SystemTabNames.Content,
           Order = 10,
           Description = "This various blocks that comprise the website's global alerts")]
        [AllowedTypes(typeof(IAlertBlock))]

        [CultureSpecific]
        public virtual ContentArea AlertContent { get; set; }
    }
}
