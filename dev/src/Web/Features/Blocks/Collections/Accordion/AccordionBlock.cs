using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Collections.Accordion
{
    /// <summary>
    /// Used to insert a accordion block component    
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Collections,
        DisplayName = "Accordion",
        GUID = "deb1e077-07e2-4cba-bec6-fd076aac6469",
        Description = "Accordion component")]
    [ImageUrl("~/icons/score/epi_score128_accordian.png")]
    [DisplayOptions(new[] {
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.Fixed })]
    [IndexInContentAreas]
    public class AccordionBlock : BaseBlock, IPageContentBlock, INestedContentBlock
    {
        [Display(
            Description = "Panels that are displayed in the accordion",
            GroupName = SystemTabNames.Content,
            Name = "Accordion Items",
            Order = 110)]
        [CultureSpecific]
        [AllowedTypes(typeof(AccordionPanelBlock))]
        [DisplayOptions(false)]
        public virtual ContentArea AccordionPanels { get; set; }
    }
}
