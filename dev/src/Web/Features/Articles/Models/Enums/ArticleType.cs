using Perficient.Web.Features.Articles.Pages.BlogDetails;
using Perficient.Web.Features.Articles.Pages.NewsDetails;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Models.Enums
{
    public enum ArticleTypes
    {
        [Display(Name = "All Articles", ResourceType = typeof(BaseArticlePage))]
        All = 0,

        [Display(Name = "Blog", ResourceType = typeof(BlogDetailsPage))]
        Blog = 1,

        [Display(Name = "News Articles", ResourceType = typeof(NewsDetailsPage))]
        NewsArticle = 2
    }
}
