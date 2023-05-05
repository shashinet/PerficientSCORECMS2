using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Fields.Vimeo
{
    public class VimeoBlockComponent : AsyncBlockComponent<VimeoBaseBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(VimeoBaseBlock currentContent)
        {
            return await Task.FromResult(View("~/Features/Blocks/Fields/Vimeo/VimeoBaseBlock.cshtml", currentContent));
        }
    }
}
