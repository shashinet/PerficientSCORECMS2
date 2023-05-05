using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.SideNavigation;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Pages.GenericLanding
{
    [ContentType(
         GroupName = GroupNames.Content,
         DisplayName = "Generic Landing Page",
         GUID = "{4CEBCB7D-F96D-4A1C-ACFB-10A85E089A5C}",
         Description = "Generic landing page used as the top level page of a site section")]
    [ImageUrl("~/icons/score/epi_score128_page_1col.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.OneFourth,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.Full })]
    public class GenericLandingPage : BasePage
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
        public virtual SideNavigationBlock SideNavigation { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            SideNavigation.NavigationMaxDepth = 3;
            base.SetDefaultValues(contentType);
        }
    }
}
