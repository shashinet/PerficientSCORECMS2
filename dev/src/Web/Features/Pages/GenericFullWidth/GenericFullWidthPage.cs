using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Pages.GenericFullWidth
{
    [ContentType(
         GroupName = GroupNames.Content,
         DisplayName = "Generic Full Width Page",
         GUID = "7186f988-e0e0-4ca3-82f2-65c87edae59d",
         Description = "Generic page template - standard page")]
    [ImageUrl("~/icons/score/epi_score128_page_1col.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.OneFourth,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.Full })]

    public class GenericFullWidthPage : BasePage
    {
        [CultureSpecific]
        [Searchable]
        [Required]
        [Display(Name = "Title",
            Description = "Page Title",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string Title { get; set; }

    }
}
