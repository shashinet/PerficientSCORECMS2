using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Components.RichText;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
        GroupName = GroupNames.Navigation,
        DisplayName = "Menu List Block",
        GUID = "{791C9EF9-CDC8-437D-977D-E9CEDFBD4F62}",
        Description = "Curated list of links with header link and icon")]
    [ImageUrl("~/icons/score/epi_score128_brandedMenu.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.OneFourth,
        DisplayOptionConstants.DisplayOptionNames.ThreeFourth })]
    public class MenuListBlock : BaseBlock, IFooterBlock, IHeaderBlock
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Section Title", Order = 20)]
        [CultureSpecific]
        public virtual string SectionTitle { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Section Url", Order = 30)]
        [CultureSpecific]
        public virtual Url SectionUrl { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Secondary Links", Order = 40)]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(NavigationLinkBlock), typeof(RichTextBlock) })]
        [DisplayOptions(false)]
        public virtual ContentArea MainContentArea { get; set; }

    }
}
