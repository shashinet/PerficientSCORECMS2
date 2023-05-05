using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Text;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Components.RichText;
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
           GroupName = SystemTabNames.Content,
           Name = "Title"
       )]
        public virtual string Title { get; set; }

        [Display(
           Order = 20,
           GroupName = SystemTabNames.Content,
           Name = "Description"
       )]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(RichTextBlock) })]
        public virtual ContentArea Description { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Name = "CTA Button", Order = 30)]
        [AllowedTypes(typeof(ICallToActionBlock))]
        public virtual ContentArea CallToActionButton { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Name = "Solid Color", Order = 30)]
        [EditorDescriptor(EditorDescriptorType = typeof(ColorPickerEditorDescriptor))]
        [UIHint("ColorPickerEditor")]
        public virtual ContentArea CallToActionButton1 { get; set; }
    }
}
