using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Articles.Blocks.ArticleCategoryLink
{
    public class ArticleCategoryLinkBlockComponent : AsyncPartialContentComponent<ArticleCategoryLinkBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(ArticleCategoryLinkBlock currentBlock)
        {
            return await Task.FromResult(View("~/Features/Articles/Blocks/ArticleCategoryLink/ArticleCategoryLinkBlock.cshtml", currentBlock));
        }
    }
}
