using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.EditorDescriptors.Themes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.Settings.Abstracts;
using Perficient.Infrastructure.Settings.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Settings.Models.Content
{
    [SettingsContentType(DisplayName = "Site Settings",
        GUID = "322f5b0b-581c-4050-90ee-d5c74f75b375",
        Description = "Core Site Settings",
        AvailableInEditMode = true,
        SettingsName = "Site Settings")]
    [ImageUrl("~/assets/icons/cms/pages/cms-icon-page-layout-settings.png")]
    public class SiteSettings : SettingsBase
    {

        [Display(Name = "Site Theme", Order = 40)]
        [SelectOne(SelectionFactoryType = typeof(SiteThemeSelectionFactory))]
        public virtual string SiteTheme { get; set; }

        [Display(Name = "Favicon",
           GroupName = SystemTabNames.Content,
           Order = 60)]
        public virtual ContentReference Favicon { get; set; }

        [Display(Name = "Breadcrumb Home Icon",
           GroupName = SystemTabNames.Content,
           Order = 70)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference BreadcrumbIcon { get; set; }

        [CultureSpecific]
        [Display(Name = "Exclude from results",
            Description = "This will determine whether or not to show on search",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        public virtual bool ExcludeFromSearch { get; set; }

        [Display(Name = "Google Tag Manager Key",
             GroupName = SystemTabNames.Content,
             Order = 80)]
        public virtual string GtmKey { get; set; }

        [Display(Name = "404 Page",
          GroupName = SystemTabNames.Content,
          Order = 120)]
        [AllowedTypes(typeof(BasePage))]
        public virtual ContentReference PageNotFound { get; set; }

        [Display(Name = "Robots Text File",
            GroupName = SystemTabNames.Content,
            Order = 140)]
        [UIHint(UIHint.Textarea)]
        public virtual string RobotsText { get; set; }
    }
}
