using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Web.Features.Blocks.Components.Image;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.FlipCardImage
{
    [ContentType(DisplayName = "Flip Card Image Block", GroupName = GroupNames.Content, GUID = "1bcc1221-efd4-4a32-8038-46fffcba1e5e", Description = "")]
    [ImageUrl("~/icons/score/epi_score128_image_feature.png")]
    public class FlipCardImageBlock : ImageBlock
    {
        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Name = "Solid Color", Order = 10)]
        [EditorDescriptor(EditorDescriptorType = typeof(SolidColorsPickerEditorDescriptor))]
        [UIHint("SolidColorsPickerEditor")]
        public virtual string SolidColor { get; set; }

        //[CultureSpecific]
        //[AllowedTypes(new[] { typeof(IconBlock) })]
        //[Display(GroupName = SystemTabNames.Content, Name = "Flip Card Icon", Order = 20)]
        //[CultureSpecific]
        //[UIHint(UIHint.Image)]
        //[AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia) })]
        //[DefaultDragAndDropTarget]
        //[FullRefresh]
        //[OptionBarItem]
        //public virtual ContentReference SmallImage { get; set; }

        //[Display(GroupName = SystemTabNames.Content, Name = "Contact us bubble icon", Order = 20)]
        //public virtual IconBlock ContactBubbleIcon { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Name = "Masked Image", Order = 30)]
        [AllowedTypes(new[] { typeof(ImageMediaData) })]
        [DefaultDragAndDropTarget]
        [UIHint(UIHint.Image)]
        public virtual ContentArea MaskedImage { get; set; }
    }
}
