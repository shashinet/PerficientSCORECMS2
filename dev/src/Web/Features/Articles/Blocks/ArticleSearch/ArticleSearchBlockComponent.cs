using EPiServer.Find.Cms;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Articles.Blocks.ArticleCategoryLink;
using Perficient.Web.Features.Articles.ViewModels;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Articles.Blocks.ArticleSearch
{
    public class ArticleSearchBlockComponent : AsyncPartialContentComponent<ArticleSearchBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(ArticleSearchBlock currentBlock)
        {
            var articleSearchBlock = currentBlock as ArticleSearchBlock;
            var articleSearchViewModel = new ArticleSearchViewModel();
            if (articleSearchBlock != null)
            {
                articleSearchViewModel = new ArticleSearchViewModel(articleSearchBlock.TypeOfArticle, articleSearchBlock.ContentTypeID, articleSearchBlock.ArticleRoot.ID, articleSearchBlock.SearchText(), articleSearchBlock.ShowSearchBox, articleSearchBlock.SearchTextPlaceholder);
            }
            return await Task.FromResult(View("~/Features/Articles/Blocks/ArticleSearch/ArticleSearchBlock.cshtml", articleSearchViewModel));
        }
    }
}
