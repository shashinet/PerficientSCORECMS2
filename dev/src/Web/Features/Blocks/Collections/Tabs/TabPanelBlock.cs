using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Components.Button;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Collections.Tabs
{
    /// <summary>
    /// Used to insert a tab panel block
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Collections,
        DisplayName = "Tab Panel",
        GUID = "45240307-4f45-41bb-9663-e5b016607c81",
        Description = "Tab panel component")]
    [ImageUrl("~/icons/score/epi_score128_tab_panel.png")]
    [IndexInContentAreas]
    public class TabPanelBlock : BaseBlock
    {
        [Display(
            Order = 50,
            Name = "Panel Button Text",
            Description = "Text that appears on button to open Tab Panel",
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        [Required]
        public virtual string PanelButtonText { get; set; }

        [Display(
            Order = 100,
            Name = "Panel Title",
            Description = "Optional title within Tab Panel content",
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        public virtual string PanelTitle { get; set; }

        [Display(
            Name = "Panel Image",
            Description = "Image that display above rich text when panel shown",
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        [UIHint(UIHint.Image)]
        [AllowedTypes(typeof(ImageMediaData))]
        [DefaultDragAndDropTarget]
        [FullRefresh]
        [OptionBarItem]
        [Required]
        public virtual ContentReference Image { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Panel Text",
            Order = 120)]
        [CultureSpecific]
        [Required]
        public virtual XhtmlString PanelText { get; set; }

        [Display(
           Name = "Panel CTA",
           Order = 130,
           GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(ButtonBlock) })]
        public virtual ContentArea CtaContentArea { get; set; }
    }
}
