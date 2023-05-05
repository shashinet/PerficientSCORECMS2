using EPiServer.Core;
using Perficient.Web.Features.Articles.ViewModels;
using System.Collections.Generic;
namespace Perficient.Web.Features.Articles.Repositories
{
    public interface IArticleRepository
    {
        public ArticleSearchResultViewModel Search(ArticleSearchFilterViewModel Filter);

        public void TrackClick(string query, string hitId, string trackId);

        public List<ArticleCategoriesViewModel> GetCategoriesForPage(ContentReference CurrentPage);
    }
}
