using System.Collections.Generic;

namespace Perficient.Web.Features.Articles.ViewModels
{
    public class ArticleSearchResultViewModel
    {
        public ArticleSearchFilterViewModel Filter { get; set; }
        public int ResultCount { get; set; }
        public IList<ArticleViewModel> Results { get; set; }
    }
}
