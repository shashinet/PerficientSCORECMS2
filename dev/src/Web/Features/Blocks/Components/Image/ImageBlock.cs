using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Image
{
    /// <summary>
    /// Used to insert a block with image
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Image",
        GUID = "71f29c58-62fb-4d51-b231-c3938ec5b4f9",
        Description = "Block with image element with options."
    )]
    [ImageUrl("~/icons/score/epi_score128_image_feature.png")]
    public class ImageBlock : BaseBlock, INestedContentBlock
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 100
        )]
        [CultureSpecific]
        [UIHint(UIHint.Image)]
        [AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia) })]
        [DefaultDragAndDropTarget]
        public virtual ContentReference Image { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Order = 30,
            Name = "Image Style"
        )]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("ImageClasses")]
        public virtual string ImageStyle { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(this.ImageStyle))
            {
                classes += $" {this.ImageStyle.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            ImageStyle = "default";
        }
    }
}
