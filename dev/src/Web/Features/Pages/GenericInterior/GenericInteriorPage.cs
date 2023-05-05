using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.SideNavigation;
using Perficient.Web.Features.Pages.GenericLanding;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Pages.GenericInterior
{
    [ContentType(
         GroupName = GroupNames.Content,
         DisplayName = "Generic Interior Page",
         GUID = "60CBD02F-55FC-4B06-93C9-2EC847FE1E5A",
         Description = "Generic interior page. This page is is part of a site section that is defined by a generic landing page")]
    [ImageUrl("~/icons/score/epi_score128_page_1col.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.OneFourth,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.Full })]

    public class GenericInteriorPage : BasePage
    {
        [CultureSpecific]
        [Searchable]
        [Required]
        [Display(Name = "Title",
            Description = "Page Title",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string Title { get; set; }

        [Display(
            Name = "Side Navigation",
            Description = "Used to create a side navigation menu on the page",
            GroupName = TabNames.Menu,
            Order = 10)]
        [ParentMenuContentType(typeof(GenericLandingPage), 3)]
        public virtual SideNavigationBlock SideNavigation { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            SideNavigation.NavigationMaxDepth = 3;
            SideNavigation.NavigationType = Blocks.Fields.SideNavigation.Enums.SideNavigationType.CurrentContent;
            base.SetDefaultValues(contentType);
        }
    }
}
