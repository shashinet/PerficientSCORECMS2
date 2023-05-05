using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Collections.Carousel
{
    /// <summary>
    /// Used to insert a carousel block
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Collections,
        DisplayName = "Carousel",
        GUID = "3b5daff5-fa09-40cb-b0cd-00de362ec879",
        Description = "Carousel component"
    )]
    [ImageUrl("~/icons/score/epi_score128_carousel.png")]
    public class CarouselBlock : BaseBlock, IPageContentBlock, IHeroBlock
    {
        [Display(
            Description = "Slides that are displayed in the carousel",
            GroupName = SystemTabNames.Content,
            Name = "Carousel Panes",
            Order = 100)]
        [CultureSpecific]
        [AllowedTypes(typeof(IHeroBlock))]
        [MaxElements(4)]
        public virtual ContentArea CarouselPanes { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Name = "Carousel Style",
            Order = 30)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("CarouselClasses")]
        public virtual string CarouselStyle { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(CarouselStyle))
            {
                classes += $" {CarouselStyle.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            CarouselStyle = "default";
        }
    }
}
