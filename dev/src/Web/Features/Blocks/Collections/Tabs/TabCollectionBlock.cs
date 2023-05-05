using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Collections.Tabs
{
    /// <summary>
    /// Used to insert a tab set block
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Collections,
        DisplayName = "Tab Set",
        GUID = "3b82e0d6-d7dd-44fa-96a7-ad490179568f",
        Description = "Tab set component")]
    [ImageUrl("~/icons/score/epi_score128_tabset.png")]
    public class TabCollectionBlock : BaseBlock, INestedContentBlock, IOnPageEditHelperPanel
    {
        [Display(
            Description = "Tab panels that are displayed in the tab set",
            GroupName = SystemTabNames.Content,
            Name = "Tab Panels",
            Order = 100)]
        [CultureSpecific]
        [AllowedTypes(typeof(TabPanelBlock))]
        public virtual ContentArea TabPanels { get; set; }
    }
}
