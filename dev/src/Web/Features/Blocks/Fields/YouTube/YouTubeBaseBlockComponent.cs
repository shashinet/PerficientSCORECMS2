using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Fields.YouTube
{
    public class YouTubeBaseBlockComponent : AsyncBlockComponent<YouTubeBaseBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(YouTubeBaseBlock currentContent)
        {
            return await Task.FromResult(View("~/Features/Blocks/Fields/YouTube/YouTubeBaseBlock.cshtml", currentContent));
        }
    }
}
