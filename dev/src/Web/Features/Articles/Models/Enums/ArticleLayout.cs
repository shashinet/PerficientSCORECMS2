using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Models.Enums
{
    public enum ArticleLayout
    {
        [Display(Name = "Grid Layout")]
        Grid,

        [Display(Name = "Help Tiles")]
        List
    }
}
