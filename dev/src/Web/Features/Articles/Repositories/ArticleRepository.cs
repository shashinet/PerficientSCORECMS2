using EPiServer;
using EPiServer.Core;
using EPiServer.Core.Internal;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework.Statistics;
using EPiServer.Find.Statistics;
using EPiServer.Globalization;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Extensions;
using Perficient.Infrastructure.Helpers;
using Perficient.Web.Features.Articles.Blocks.ArticleCategoryLink;
using Perficient.Web.Features.Articles.Extensions;
using Perficient.Web.Features.Articles.Models;
using Perficient.Web.Features.Articles.Models.Enums;
using Perficient.Web.Features.Articles.Pages.ArticleCategoryLanding;
using Perficient.Web.Features.Articles.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Web.Features.Articles.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IClient _searchClient;
        private readonly Language _language;
        private readonly IContentLoader _contentLoader;
        private readonly IStatisticTagsHelper _statisticsTagsHelper;


        public ArticleRepository(ContentLoader contentLoader, IClient searchClient, IStatisticTagsHelper statisticsTagsHelper)
        {
            _searchClient = searchClient;
            _language = _searchClient.Settings.Languages.GetSupportedLanguage(ContentLanguage.PreferredCulture) ?? Language.None;
            _contentLoader = contentLoader;
            _statisticsTagsHelper = statisticsTagsHelper;
        }

        public ArticleSearchResultViewModel Search(ArticleSearchFilterViewModel Filter)
        {
            var results = new ArticleSearchResultViewModel();
            var trackId = string.Empty;
            var selectedArticleType = EnumHelper<ArticleTypes>.GetDisplayTypeValue(Filter.ArticleType);
            var query = GetSearchQueryable(out trackId, Filter.SearchTerm, Filter.Track);
            Filter.TrackId = trackId;

            //Filter on article type
            query = query.Filter(x => x.MatchTypeHierarchy(selectedArticleType));

            //Filter on Search Term
            if (!string.IsNullOrEmpty(Filter.SearchTerm))
            {
                query = query.For(Filter.SearchTerm); 
            }
           
            query = query.Filter(x => x.MatchTypeHierarchy(selectedArticleType));
            

            //Filter on the Pages under the Page Id provided
            if (Filter.RootPageId > 0)
            {
                query = query.Filter(x => x.Ancestors().Match(Filter.RootPageId.ToString()));
            }

            //TODO - Filter on Cateogry
            if (Filter.CategoryId > 0 )
            {
                
                query = query.Filter(x => x.ArticleCategory.ID.Match(Filter.CategoryId));
            }

            var sortedQuery = query
                .OrderByScore();

            switch (Filter.Sort)
            {
                case ArticleSortBy.DateDescending:
                    sortedQuery = sortedQuery.ThenByDescending(x => x.PublishedDate);
                    break;
                case ArticleSortBy.DateAscending:
                    sortedQuery = sortedQuery.ThenBy(x => x.PublishedDate);
                    break;
                case ArticleSortBy.TitleDescending:
                    sortedQuery = sortedQuery.ThenByDescending(x => x.Title);
                    break;
                case ArticleSortBy.TitleAscending:
                    sortedQuery = sortedQuery.ThenBy(x => x.Title);
                    break;
                default:
                    break;
            }

            var contentResult = sortedQuery
                .Skip(Filter.PageNumber <= 1 ? 0 : (Filter.PageNumber - 1) * Filter.PageSize)
                .Take(Filter.PageSize)
                .GetContentResult();
                        
            var hits = contentResult
                .Select(x => new ArticleViewModel() {
                    Id = x.ContentLink.ID,
                    ArticleUrl = UrlResolver.Current.GetUrl(x),
                    Title = x.Title,
                    Summary = x.Summary,
                    Category = x.ArticleCategory.GetArticleCategory()?.Name,
                    CategoryColor = x.ArticleCategory.GetArticleCategory()?.Color,
                    ImageUrl = UrlResolver.Current.GetUrl(x.ContentImage.GetCroppingForDevice(ContentImageNames.Card)),
                    PublishDate = x.PublishedDate
                })
                .ToList();

            results.Filter = Filter;
            results.ResultCount = contentResult.TotalMatching;            
            results.Results = hits;

            return results;
        }

        private ITypeSearch<BaseArticlePage> GetSearchQueryable(out string trackId, string query = null, bool track = false)
        {
            trackId = new TrackContext().Id;
            var siteId = SiteDefinition.Current.Id;

            var client = _searchClient.Search<BaseArticlePage>(_language);

            if (!string.IsNullOrEmpty(query))
            {
                client = client
                    .For(query)
                    .InField(x => x.Name)
                    .UsingSynonyms()
                    .ApplyBestBets()
                    .WildcardSearch(query,
                        (x => x.Title, 2),
                        (x => x.Keywords, 1.5),
                        (x => x.Summary, null),
                        (x => x.SearchText(), null)
                    );
            }

            if (track)
            {
                client = client.Track(trackId);
            }

            return client
                    .FilterForVisitor()
                    .ExcludeDeleted()
                    .CurrentlyPublished()
                    .Filter(x => x.ExcludeFromSearch.Match(false));
        }

        public void TrackClick(string query, string hitId, string trackId)
        {
            _searchClient.Statistics().TrackHit(query, hitId, command =>
            {
                command.Hit.Id = hitId;
                command.Id = trackId;
                command.Tags = _statisticsTagsHelper.GetTags(false);
                command.Hit.Position = null;
            });
        }
        
        public List<ArticleCategoriesViewModel> GetCategoriesForPage(ContentReference currentPage)
        {
            var articleHomePage = currentPage.GetContentAncestors<ArticleHomePage>().FirstOrDefault();
            var categories = new List<ArticleCategoriesViewModel>();

            if (articleHomePage == null)
            {
                return categories;
            }

            foreach (var item in articleHomePage?.ArticleCategories?.Items)
            {
                var categoryLinkBlock = _contentLoader.Get<ArticleCategoryLinkBlock>(item.ContentLink);
                var categoryPage = _contentLoader.Get<ArticleCategoryLandingPage>(categoryLinkBlock.CategoryLandingPage);
                var articleCateogry = _contentLoader.Get<ArticleCategory>(categoryLinkBlock.Category);

                categories.Add(new ArticleCategoriesViewModel()
                {
                    Title = articleCateogry.Name,
                    Color = articleCateogry.Color,
                    Url = categoryPage.ContentLink != ContentReference.EmptyReference ? UrlResolver.Current.GetUrl(categoryPage.ContentLink) : "",
                    Id = articleCateogry.ContentGuid
                });
            }

            return categories;
        }

        private ITypeSearch<T> CreateQueryForCurrentLanguage<T>()
        {
            return _searchClient.Search<T>(_language);
        }

    }
}
