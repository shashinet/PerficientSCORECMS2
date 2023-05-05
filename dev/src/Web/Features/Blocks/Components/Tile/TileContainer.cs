using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Tile
{
    /// <summary>
    /// Used to insert a accordion block component
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Buttons,
        DisplayName = "Tile Container",
        GUID = "{E48C8553-94AD-467C-8A32-373B3F54D5A7}",
        Description = "Container for tile blocks")]
    [ImageUrl("~/icons/score/epi_score128_page_3colEqual.png")]
    
    [DisplayOptions(new[] {
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.Fixed })]
    public class TileContainer : BaseBlock, INestedContentBlock
    {
        [Display(
            Description = "Container for tile blocks",
            GroupName = SystemTabNames.Content,
            Name = "Tile Blocks",
            Order = 110)]
        [CultureSpecific]
        [AllowedTypes(typeof(TileBlock))]
        [DisplayOptions(true)]
        public virtual ContentArea TileItems { get; set; }
    }
}
