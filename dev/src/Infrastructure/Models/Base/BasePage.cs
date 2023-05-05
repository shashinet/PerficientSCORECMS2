using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.Sitemaps.SpecializedProperties;
using Newtonsoft.Json;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Perficient.Infrastructure.Templates.Interfaces;

namespace Perficient.Infrastructure.Models.Base
{
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.OneFourth,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.ThreeFourth,
        DisplayOptionConstants.DisplayOptionNames.Full })]
    public abstract class BasePage : PageData, ITemplatePage
    {        
        #region Content

        [CultureSpecific]
        [Searchable]
        [Display(Name = "Main content area", GroupName = SystemTabNames.Content, Order = 100)]
        [AllowedTypes(new[] { typeof(IPageContentBlock), typeof(BasePage), typeof(IFormContainerBlock) })]
        [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.EdgeToEdge,
            DisplayOptionConstants.DisplayOptionNames.Contained,
            DisplayOptionConstants.DisplayOptionNames.Offset,
            DisplayOptionConstants.DisplayOptionNames.Fixed })]
        public virtual ContentArea MainContentArea { get; set; }

        #endregion Content

        #region Metadata

        [CultureSpecific]
        [Display(Name = "Meta Title", GroupName = TabNames.MetaData, Order = 100)]
        public virtual string MetaTitle
        {
            get
            {
                var metaTitle = this.GetPropertyValue(p => p.MetaTitle);

                return !string.IsNullOrWhiteSpace(metaTitle)
                    ? metaTitle
                    : PageName;
            }
            set => this.SetPropertyValue(p => p.MetaTitle, value);
        }

        [Display(Name = "Navigation Title",
            Description = "Overrides the title in the Navigation Blocks",
            GroupName = TabNames.MetaData,
            Order = 2)]
        [CultureSpecific]
        [Searchable]
        public virtual string NavigationTitle { get; set; }

        [Display(Name = "Canonical URL",
            GroupName = TabNames.MetaData,
            Order = 5)]
        public virtual string CanonicalUrlOverride { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(GroupName = TabNames.MetaData, Order = 200)]
        public virtual string Keywords { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Page description", GroupName = TabNames.MetaData, Order = 300)]
        public virtual string PageDescription { get; set; }

        [CultureSpecific]
        [Display(Name = "Content type", GroupName = TabNames.MetaData, Order = 310)]
        public virtual string MetaContentType { get; set; }

        [CultureSpecific]
        [Display(Name = "Author", GroupName = TabNames.MetaData, Order = 320)]
        public virtual string AuthorMetaData { get; set; }

        [CultureSpecific]
        [Display(Name = "Disable indexing", GroupName = TabNames.MetaData, Order = 400)]
        public virtual bool DisableIndexing { get; set; }

        #endregion Metadata

        #region Settings

        [CultureSpecific]
        [Display(Name = "Exclude from Search Results",
            Description = "This will determine whether or not to show on search",
            GroupName = TabNames.Settings,
            Order = 100)]
        public virtual bool ExcludeFromSearch { get; set; }

        [CultureSpecific]
        [Display(Name = "Hide site header", GroupName = TabNames.Settings, Order = 200)]
        public virtual bool HideSiteHeader { get; set; }

        [CultureSpecific]
        [Display(Name = "Hide site footer", GroupName = TabNames.Settings, Order = 300)]
        public virtual bool HideSiteFooter { get; set; }

        [CultureSpecific]
        [Display(Name = "Sitemap Settings",
        Description = "This will exclude the content from Sitemap",
        GroupName = TabNames.Settings,
        Order = 400)]
        [UIHint("SeoSitemap")]
        [BackingType(typeof(PropertySEOSitemaps))]
        public virtual string SEOSitemaps { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            var sitemap = new PropertySEOSitemaps
            {
                Enabled = false
            };
            sitemap.Serialize();
            SEOSitemaps = sitemap.ToString();
        }

        // TODO: will consider showing this property in Edit Mode
        //[Display(GroupName = TabNames.TemplateSettings, Name = "Selected Template", Order = 10)]
        [AllowedTypes(typeof(ITemplatePage))]
        [ScaffoldColumn(false)]
        public virtual ContentReference SelectedTemplate { get; set; }

        [AllowedTypes(typeof(ITemplatePage))]
        [ScaffoldColumn(false)]
        public virtual ContentReference OldTemplate { get; set; }

        #endregion Settings

        #region Styles

        [Display(Name = "CSS files", GroupName = TabNames.Css, Order = 100)]
        public virtual LinkItemCollection CssFiles { get; set; }

        [Display(Name = "CSS", GroupName = TabNames.Css, Order = 200)]
        [UIHint(UIHint.Textarea)]
        public virtual string Css { get; set; }

        #endregion Styles

        #region Scripts

        [Display(Name = "Script files", GroupName = TabNames.Scripts, Order = 100)]
        public virtual LinkItemCollection ScriptFiles { get; set; }

        [UIHint(UIHint.Textarea)]
        [Display(GroupName = TabNames.Scripts, Order = 200)]
        public virtual string Scripts { get; set; }

        #endregion Scripts

        #region Page Header

        [JsonIgnore]
        [ScaffoldColumn(false)]
        public virtual string NavigationHrefTarget
        {
            get
            {
                return "";
            }
        }

        [JsonIgnore]
        [ScaffoldColumn(false)]
        [Display(AutoGenerateField = false)]
        public override bool VisibleInMenu { get; set; }

        #endregion

        public string AssetVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}
