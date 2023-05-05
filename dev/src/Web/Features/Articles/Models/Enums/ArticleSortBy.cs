using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Models.Enums
{
    public enum ArticleSortBy
    {
        [Display(Description = "Default", Name = "Default")]
        None = 0,

        [Display(Description = "Ascending Date", Name = "Ascending Date")]
        DateAscending = 1,

        [Display(Description = "Descending Date", Name = "Descending Date")]
        DateDescending = 2,

        [Display(Description = "Ascending Title", Name = "Ascending Title")]
        TitleAscending = 3,

        [Display(Description = "Descending Title", Name = "Descending Title")]
        TitleDescending = 4
    }
}
