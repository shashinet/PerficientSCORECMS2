using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Components.Tile;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
    GroupName = GroupNames.Navigation,
    DisplayName = "Navigation Panel",
    GUID = "{7F943C93-145C-4143-8D8B-C83B9C531A1E}",
    Description = "Site footer block")]
    [ImageUrl("~/icons/score/nav-list.png")]
    [DisplayOptions(new[] {
        DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.ThreeFourth,
        DisplayOptionConstants.DisplayOptionNames.OneFourth })]
    public class NavigationPanelBlock : BaseBlock, IFooterBlock, IHeaderBlock
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Main Content Area",
            Order = 10)]
        [CultureSpecific]
        [AllowedTypes(typeof(MenuListBlock), typeof(TileBlock) )]
        [DisplayOptions(false)]
        public virtual ContentArea MainContentArea { get; set; }
    }
}
