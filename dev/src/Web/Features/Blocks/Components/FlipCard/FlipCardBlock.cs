using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Text;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Components.RichText;
using Perficient.Web.Features.Blocks.Fields.Icon;
using Perficient.Web.Features.Media;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;

namespace Perficient.Web.Features.Blocks.Components.FlipCard
{
    [ContentType(
       GroupName = GroupNames.Content,
       DisplayName = "FlipCard",
       GUID = "49483866-ca15-48ca-a869-16cb2e4971a9",
       Description = "FlipCard component"
   )]
    [ImageUrl("~/icons/score/epi_score128_masterHeader.png")]
    public class FlipCardBlock : BaseBlock, IPageContentBlock
    {
        [CultureSpecific]
        [Display(
           Order = 10,
           GroupName = TabNames.FrontOfCard,
           Name = "Title"
       )]
        public virtual string Title { get; set; }

        [Display(
           Order = 20,
           GroupName = TabNames.FrontOfCard,
           Name = "Description"
       )]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(RichTextBlock) })]
        public virtual ContentArea Description { get; set; }

        [CultureSpecific]
        [Display(GroupName = TabNames.FrontOfCard, Name = "Button Label", Order = 30)]
      
        public virtual string CallToActionLabel { get; set; }

        //[CultureSpecific]
        //[Display(GroupName = TabNames.FrontOfCard, Name = "Image", Order = 40)]
        //[AllowedTypes(new[] {typeof(FlipCardImageBlock) })]
        //public virtual ContentArea Image { get; set; }
        [CultureSpecific]
        [Display(GroupName = TabNames.FrontOfCard, Name = "Solid Color", Order = 40)]
        [EditorDescriptor(EditorDescriptorType = typeof(SolidColorsPickerEditorDescriptor))]
        [UIHint("SolidColorsPickerEditor")]
        public virtual string FrontSolidColor { get; set; }

        [CultureSpecific]
        [Display(GroupName = TabNames.FrontOfCard, Name = "Small Image", Order = 50)]
        [UIHint(UIHint.Image)]
        [AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia) })]
        [DefaultDragAndDropTarget]
        [FullRefresh]
        [OptionBarItem]
        public virtual ContentReference SmallImage { get; set; }

        [CultureSpecific]
        [Display(GroupName = TabNames.FrontOfCard, Name = "Masked Image", Order = 60)]
        [AllowedTypes(new[] { typeof(ImageMediaData) })]
        [DefaultDragAndDropTarget]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MaskedImage { get; set; }

        [CultureSpecific]
        [Display(GroupName = TabNames.BackOfCard, Name = "Solid Color", Order = 10)]
        [EditorDescriptor(EditorDescriptorType = typeof(SolidColorsPickerEditorDescriptor))]
        [UIHint("SolidColorsPickerEditor")]
        public virtual string BackSolidColor { get; set; }       

        [Display(
          Order = 20,
          GroupName = TabNames.BackOfCard,
          Name = "Text"
      )]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(RichTextBlock) })]
        public virtual ContentArea Text { get; set; }

        [CultureSpecific]
        [Display(GroupName = TabNames.BackOfCard, Name = "CTA Button", Order = 30)]
        [AllowedTypes(typeof(ICallToActionBlock))]
        public virtual ContentArea CallToActionButton { get; set; }
    }
}
