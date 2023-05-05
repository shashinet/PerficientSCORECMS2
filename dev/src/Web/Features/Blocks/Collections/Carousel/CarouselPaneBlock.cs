using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Collections.Carousel
{
    /// <summary>
    /// Used to insert a carousel pane block
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Collections,
        DisplayName = "Carousel Pane",
        GUID = "8f01a0f8-1b0d-4db4-b53f-667ce3e1a69d",
        Description = "Carousel pane component"
    )]
    [ImageUrl("~/icons/score/epi_score128_carousel_pane.png")]
    public class CarouselPaneBlock : BaseBlock, IHeroBlock, IOnPageEditHelperPanel
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Background Image",
            Order = 100)]
        [CultureSpecific]
        [UIHint(UIHint.Image)]
        [DefaultDragAndDropTarget]
        public virtual ContentReference BackgroundImage { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Name = "Text Color",
            Order = 10)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(FontColorPickerEditorDescriptor))]
        [UIHint("FontColorPickerEditor")]
        public virtual string TextColor { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Name = "Background Color",
            Order = 20)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(ColorPickerEditorDescriptor))]
        [UIHint("ColorPickerEditor")]
        public virtual string BackgroundColor { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Content Area",
            Order = 130)]
        [CultureSpecific]
        [AllowedTypes(typeof(INestedContentBlock))]
        public virtual ContentArea ContentArea { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Name = "Carousel Pane Classes",
            Order = 30)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("CarouselPaneClasses")] // name of the class selection property on ScoreSettingsPage
        public virtual string CarouselPaneStyle { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(CarouselPaneStyle))
            {
                classes += $" {CarouselPaneStyle.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            CarouselPaneStyle = "default";
        }
    }
}
