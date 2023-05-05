using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Articles.Blocks.ArticlesTopPosts;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Articles.Blocks.ArticleCategoryLink
{
    public class ArticleTopPostsBlockComponent : AsyncPartialContentComponent<ArticleTopPostBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(ArticleTopPostBlock currentBlock)
        {
            return await Task.FromResult(View("~/Features/Articles/Blocks/ArticleTopPosts/ArticleTopPostBlock.cshtml", currentBlock));
        }
    }
}
