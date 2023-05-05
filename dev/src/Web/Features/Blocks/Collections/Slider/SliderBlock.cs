using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Components.Button;
using Perficient.Web.Features.Blocks.Fields.Heading;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Collections.Slider
{
    [ContentType(
        GroupName = GroupNames.Collections,
        DisplayName = "Slider Block",
        Description = "Navigable collection of cards.",
        GUID = "A1B9A348-07AC-4D4D-A66E-B5F1E259C509"
    )]
    [ImageUrl("~/icons/score/epi_score128_carousel.png")]
    public class SliderBlock : BaseBlock, IPageContentBlock, IOnPageEditHelperPanel
    {
        [Display(
            Name = "Title",
            Description = "Title Above Slider",
            GroupName = SystemTabNames.Content,
            Order = 100
        )]
        public virtual HeadingBlock Title { get; set; }

        [Display(
            Name = "Slider Cards",
            Description = "Cards that populate slider.",
            GroupName = SystemTabNames.Content,
            Order = 200
        )]
        [CultureSpecific]
        [AllowedTypes(typeof(ICardBlock))]
        public virtual ContentArea Cards { get; set; }

        [Display(
           Name = "Slider CTA Area",
           Description = "Optional CTA to display with slider",
           GroupName = SystemTabNames.Content,
           Order = 300
        )]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(ButtonBlock) })]
        public virtual ContentArea CtaContentArea { get; set; }

        [Display(
            Name = "Show dotted pagination",
            Description = "Enable to show dotted pagination on slider",
            GroupName = SystemTabNames.Content,
            Order = 400)]
        [OptionBarItem]
        [CultureSpecific]
        public virtual bool ShowPagination { get; set; }

    }
}
