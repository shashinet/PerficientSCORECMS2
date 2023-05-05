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
using Perficient.Web.Features.Blocks.Components.Button;
using Perficient.Web.Features.Blocks.Fields.Heading;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.PricingCard
{
    [ContentType(
    GroupName = GroupNames.Content,
    DisplayName = "Pricing Card",
    GUID = "0C06B2D9-57DB-4FB8-9A87-164240F3CD98",
    Description = "Card for listing out service details and price"
    )]
    [ImageUrl("~/icons/score/epi_score128_highlight.png")]
    public class PricingCardBlock : BaseBlock, INestedContentBlock
    {
        [Display(
            Order = 10,
            GroupName = SystemTabNames.Content,
            Name = "Title"
        )]
        public virtual string MainTitle { get; set; }

        [Display(
            Order = 20,
            GroupName = SystemTabNames.Content,
            Name = "SubTitle"
        )]
        public virtual string SecondTitle { get; set; }


        [Display(GroupName = SystemTabNames.Content, Order = 30)]
        [UIHint(UIHint.Image)]
        [AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia) })]
        [DefaultDragAndDropTarget]
        [CultureSpecific]
        public virtual ContentReference Image { get; set; }

        [Display(
        Order = 30,
        GroupName = SystemTabNames.Content,
        Name = "Price"
        )]
        public virtual string Price { get; set; }

        [Display(
        Order = 40,
        GroupName = SystemTabNames.Content,
        Name = "Price Term"
        )]
        public virtual string PriceTerm { get; set; }


        [Display(
        Order = 50,
        GroupName = SystemTabNames.Content,
        Name = "Details"
        )]
        public virtual XhtmlString Details { get; set; }

        [Display(
        Order = 60,
        GroupName = SystemTabNames.Content,
        Name = "CTA Button"
        )]
        public virtual ButtonBlock CtaButton { get; set; }

        [Display(
        Order = 70,
        GroupName = TabNames.Experiment,
        Name = "Feature Experimentation Key"
        )]
        public virtual string ExperimentationKey { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Order = 30,
            Name = "Card Style"
        )]

        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("PricingCardClasses")]
        public virtual string PricingCardStyle { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(PricingCardStyle))
            {
                classes += $" {PricingCardStyle.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            MainTitle = "[Pricing Card]";
            PricingCardStyle = "default";
        }
    }
}
