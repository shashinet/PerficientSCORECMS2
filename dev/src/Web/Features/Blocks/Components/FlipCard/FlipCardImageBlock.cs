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
using Perficient.Web.Features.Blocks.Components.Image;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.FlipCard
{
    [ContentType(DisplayName = "Flip Card Image Block", GroupName = GroupNames.Content, GUID = "1bcc1221-efd4-4a32-8038-46fffcba1e5e", Description = "")]
    [ImageUrl("~/icons/score/epi_score128_image_feature.png")]
    public class FlipCardImageBlock : BaseBlock, INestedContentBlock
    {
        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Name = "Solid Color", Order = 10)]
        [EditorDescriptor(EditorDescriptorType = typeof(SolidColorsPickerEditorDescriptor))]
        [UIHint("SolidColorsPickerEditor")]
        public virtual string SolidColor { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Name = "Small Image", Order = 20)]
        [UIHint(UIHint.Image)]
        [AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia) })]
        [DefaultDragAndDropTarget]
        [FullRefresh]
        [OptionBarItem]
        public virtual ContentReference SmallImage { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Name = "Masked Image", Order = 30)]
        [AllowedTypes(new[] { typeof(ImageMediaData) })]
        [DefaultDragAndDropTarget]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MaskedImage { get; set; }
    }
}
