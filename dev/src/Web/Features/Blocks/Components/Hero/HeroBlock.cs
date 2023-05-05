using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.Heading;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Hero
{
    /// <summary>
    /// Used to insert a hero block component
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Hero",
        GUID = "39483886-ba15-45ca-a868-12cb2e4971a8",
        Description = "Hero component for Carousel Blocks"
    )]
    [ImageUrl("~/icons/score/epi_score128_masterHeader.png")]
    public class HeroBlock : BaseBlock, IHeroBlock, IPageContentBlock
    {
        [Display(
            Name = "Title",
            Order = 10)]
        [HeadingStyles("H1", "H2")]
        public virtual HeadingBlock HeaderTitle { get; set; }

        [Display(
            Name = "Image",
            GroupName = SystemTabNames.Content,
            Order = 10
        )]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(ImageMediaData) })]
        [DefaultDragAndDropTarget]
        [UIHint(UIHint.Image)]
        [OptionBarItem]
        [FullRefresh]
        public virtual ContentReference Media { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 40)]
        [CultureSpecific]
        [Searchable]
        public virtual XhtmlString Body { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Call To Action Content Area",
            Order = 50)]
        [CultureSpecific]
        [AllowedTypes(typeof(ICallToActionBlock))]
        public virtual ContentArea CallToActionContentArea { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Order = 70,
            Name = "Text Color")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(FontColorPickerEditorDescriptor))]
        [UIHint("FontColorPickerEditor")]
        [OptionBarItem]
        [FullRefresh]
        public virtual string TextColor { get; set; }

        [Display(GroupName = TabNames.Styles, Order = 80, Name = "Background Color")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(ColorPickerEditorDescriptor))]
        [UIHint("ColorPickerEditor")]
        [OptionBarItem]
        [FullRefresh]
        public virtual string BackgroundColor { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            HeaderTitle.HeadingStyle = "H2";
            HeaderTitle.Text = "[Hero]";
        }
    }
}
