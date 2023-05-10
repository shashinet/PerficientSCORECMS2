using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Models.Properties;
using Perficient.Infrastructure.Settings.Abstracts;
using Perficient.Infrastructure.Settings.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Perficient.Infrastructure.EditorDescriptors.PropertyList;

namespace Perficient.Infrastructure.Settings.Models.Content
{
    [SettingsContentType(
        DisplayName = "Style Settings",
        GUID = "016b7c67-12c3-4a3a-885e-fbf302f135c3",
        Description = "Settings for global styles and block styles",
        SettingsName = "Style Settings",
        AvailableInEditMode = true)]
    [ImageUrl("~/icons/cms/pages/cms-icon-page-layout-settings.png")]
    public class StyleSettings : SettingsBase
    {
        [Display(Name = "Colors")]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreColor>))]
        public virtual IList<ScoreColor> Colors { get; set; }

        [Display(Name = "Font Colors")]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreColor>))]
        public virtual IList<ScoreColor> FontColors { get; set; }

        [Display(Name = "Buttons", GroupName = TabNames.Styles, Order = 10)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreButtonStyle>))]
        public virtual IList<ScoreButtonStyle> ButtonStyles { get; set; }

        [Display(Name = "Images", GroupName = TabNames.Styles, Order = 40)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> ImageClasses { get; set; }

        [Display(Name = "Callouts", GroupName = TabNames.Styles, Order = 50)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> CalloutClasses { get; set; }

        [Display(Name = "Carousels", GroupName = TabNames.Styles, Order = 60)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> CarouselClasses { get; set; }

        [Display(Name = "Carousel Panes", GroupName = TabNames.Styles, Order = 65)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> CarouselPaneClasses { get; set; }

        [Display(Name = "Document Headers", GroupName = TabNames.Styles, Order = 65)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> DocumentHeaderClasses { get; set; }

        [Display(Name = "Page Layouts", GroupName = TabNames.Styles, Order = 70)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> PageLayoutClasses { get; set; }

        [Display(Name = "Cards", GroupName = TabNames.Styles, Order = 80)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> CardClasses { get; set; }

        [Display(Name = "Pricing Cards", GroupName = TabNames.Styles, Order = 80)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> PricingCardClasses { get; set; }

        [Display(Name = "Tiles", GroupName = TabNames.Styles, Order = 85)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> TileClasses { get; set; }

        [Display(Name = "Styleboxes", GroupName = TabNames.Styles, Order = 90)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> StyleBoxClasses { get; set; }

        [Display(Name = "Horizontal Rules", GroupName = TabNames.Styles, Order = 95)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> HorizontalRuleClasses { get; set; }

        [Display(Name = "Section Hero", GroupName = TabNames.Styles, Order = 105)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> SectionHeroClasses { get; set; }

        [Display(Name = "Stripes", GroupName = TabNames.Styles, Order = 100)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> StripeClasses { get; set; }

        [Display(Name = "Site Footer", GroupName = TabNames.Styles, Order = 115)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> FooterClasses { get; set; }

        [Display(Name = "Site Header", GroupName = TabNames.Styles, Order = 120)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> HeaderClasses { get; set; }

        [Display(Name = "Side Navigation", GroupName = TabNames.Styles, Order = 125)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> SideNavClasses { get; set; }

        [Display(Name = "Global (all blocks)", GroupName = TabNames.Styles, Order = 200)]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> StyleClasses { get; set; }

        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreButtonSize>))]
        public virtual IList<ScoreButtonSize> ButtonSizes { get; set; }

        [Display(Name = "Solid Colors")]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreColor>))]
        public virtual IList<ScoreColor> SolidColors { get; set; }

        [Display(Name = "Filip Card Directions")]
        [EditorDescriptor(EditorDescriptorType = typeof(PropertyListEditorDescriptor<ScoreClass>))]
        public virtual IList<ScoreClass> FlipCardDirectionClasses { get; set; }

    }
}
