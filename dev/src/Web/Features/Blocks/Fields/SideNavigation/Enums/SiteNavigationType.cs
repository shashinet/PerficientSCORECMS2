using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.Enums
{
    public enum SideNavigationType
    {
        [Display(Name = "Content Area", Description = "Build via Content Area")]
        ContentArea,

        [Display(Name = "Top Level Links", Description = "Select Top Level Links")]
        TopLevelLinks,

        [Display(Name = "Current Content", Description = "Generate From Current Content")]
        CurrentContent,
    }
}
