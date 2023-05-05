using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;


namespace Perficient.Web.Features.Blocks.Collections.Accordion
{
    /// <summary>
    /// Used to insert a accordion panel block
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Collections,
        DisplayName = "Accordion Panel",
        GUID = "9d928e00-cd7f-4ef3-a86d-be0a54208bd9",
        Description = "Accordion panel component")]
    [ImageUrl("~/icons/score/epi_score128_accordian_panel.png")]    
    public class AccordionPanelBlock : BaseBlock, IOnPageEditHelperPanel
    {
        [Display(
            Name = "Accordion Title",
            Order = 100,
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        public virtual string Title { get; set; }

        [Display(
            Name = "Accordion Content Heading",
            Order = 110,
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        public virtual string ContentHeading { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Rich Content Area",
            Order = 120)]
        [CultureSpecific]
        public virtual XhtmlString RichContent { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Content Area",
            Order = 130)]
        [CultureSpecific]
        [AllowedTypes(typeof(INestedContentBlock))]
        public virtual ContentArea ContentArea { get; set; }

        [Display(
            Name = "Open on Page Load",
            Order = 140,
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        [OptionBarItem]
        public virtual bool OpenOnLoad { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            OpenOnLoad = false;
        }
    }
}
