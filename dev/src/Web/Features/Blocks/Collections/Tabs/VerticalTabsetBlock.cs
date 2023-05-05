using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Components.Button;
using Perficient.Web.Features.Blocks.Fields.Heading;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Collections.Tabs
{
    /// <summary>
    /// Used to insert a vertical tab collection panel block
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Collections,
        DisplayName = "Vertical Tab Container",
        GUID = "20C74C1F-B00B-4003-ADB1-9C82B4EC969F",
        Description = "Vertical Tab Collection component")]
    [ImageUrl("~/icons/score/epi_score128_tab_panel.png")]
    public class VerticalTabsetBlock : BaseBlock, IPageContentBlock, IOnPageEditHelperPanel
    {
        [Display(
             Order = 100,
             GroupName = SystemTabNames.Content
         )]
        public virtual HeadingBlock Title { get; set; }

        [Display(
            Order = 200,
            GroupName = SystemTabNames.Content
        )]
        public virtual HeadingBlock Subtitle { get; set; }

        [Display(
           Name = "Tab Panels Content Area",
           Order = 300,
           GroupName = SystemTabNames.Content
       )]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(TabPanelBlock) })]
        public virtual ContentArea TabPanelsContentArea { get; set; }

        [Display(
           Name = "CTA Content Area",
           Order = 400,
           GroupName = SystemTabNames.Content
       )]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(ButtonBlock) })]
        public virtual ContentArea CtaContentArea { get; set; }
    }
}
