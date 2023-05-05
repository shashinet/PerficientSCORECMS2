using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.Models
{
    [MaxNestingDepth]
    [ContentType(
        GroupName = GroupNames.Navigation,
        DisplayName = "Side Navigation Link Item",
        GUID = "C43DBF5D-3CBB-48BF-BEB6-A9BBD9CF91FB")]
    [ImageUrl("~/icons/score/single-nav-list.png")]
    public class SideNavigationLinkItem : BlockData, INestedContentBlock
    {
        [Display(
            Name = "Nav Title",
            Order = 10,
            Description = "Text for the Title for the Navigation node")]
        [CultureSpecific]
        [OptionBarItem]
        [Required]
        public virtual string NavTitle { get; set; }

        [Display(
            Name = "Main Item Link",
            Order = 20,
            Description = "This is the URL for the main item")]
        [OptionBarItem(DisplayText = "Edit Main Item Link")]
        [CultureSpecific]
        [Required]
        public virtual Url NavItemLink { get; set; }

        [Display(
            Name = "Nav Aria Text",
            Order = 30,
            Description = "Aria Text for the Title for the Navigation node")]
        [CultureSpecific]
        [OptionBarItem]
        [Required]
        public virtual string NavAriaText { get; set; }

        [Display(
            Name = "Open Main Item in new window",
            Order = 40,
            Description = "Should the main item nav link open in a new window")]
        [OptionBarItem(DisplayText = "Open Main Item in New Window?")]
        [CultureSpecific]
        [Required]
        public virtual bool OpenLinkInNewWindow { get; set; }

        [Display(
            Name = "Child Nav Links",
            Order = 50,
            Description = "Text and URL for the navigation links")]
        [AllowedTypes(new[] { typeof(SideNavigationLinkItem) })]
        [CultureSpecific]
        [OptionBarItem]
        public virtual ContentArea NavItemChildLinks { get; set; }
    }
}
