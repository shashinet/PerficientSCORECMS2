using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Attributes;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Enums;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation
{
    [MaxNestingDepth]
    [ContentType(
        DisplayName = "Side Navigation Block",
        GUID = "42069749-9694-4DAC-BD45-0957CF4B5B4D",
        Description = "Used to create a side navigation menu on a page",
        AvailableInEditMode = false)]
    public class SideNavigationBlock : BlockData
    {
        /* Developer Note:
         * 
         * The max range of 5 here has been set for performance reasons, 
         * especially with a lot of menu items at each level.
         * 
         * Feel free to adjust as you see fit for your project.
         * 
         */
        [Display(
            Name = "Navigation Max Depth",
            Description = "Sets the maximum depth of the menu tiers",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [Range(0, 5)]
        public virtual int NavigationMaxDepth { get; set; }

        [Display(
            Name = "Side Navigation Style",
            Description = "Specific Styles for Side Navigation Block",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("SideNavClasses")] // name of the class selection property on ScoreSettingsPage
        public virtual string SideNavStyle { get; set; }

        [Display(
            Name = "Side Navigation Type",
            Description = "Select to manually build the menu with a content area or to have it built automatically " +
                            "from the current content or selected top level links.",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        [EnumSelect(typeof(SideNavigationType))]
        public virtual SideNavigationType NavigationType { get; set; }

        [Display(
            Name = "Menu Build Direction",
            Description = "Select whether to use this content's children or ancestors to build the menu.",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        [QuickSelect("Up", "Down")]
        public virtual string MenuDirection { get; set; }

        [Display(
            Name = "Top Level Links",
            Description = "Used to set top level links when building the menu from site architecture",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        [AllowedTypes(new[] { typeof(BasePage) })]
        public virtual IList<ContentReference> TopLevelLinks { get; set; }

        [Display(
            Name = "Navigation Items",
            Description = "Used to manually create the side navigation when building the menu from content area.",
            GroupName = SystemTabNames.Content,
            Order = 60)]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(SideNavigationLinkItem) })]
        public virtual ContentArea NavigationItems { get; set; }
    }
}
