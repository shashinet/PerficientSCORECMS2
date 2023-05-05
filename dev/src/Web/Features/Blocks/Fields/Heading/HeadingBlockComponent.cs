using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Fields.Heading
{
    public class HeadingBlockComponent : AsyncBlockComponent<HeadingBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(HeadingBlock currentContent)
        {
            return await Task.FromResult(View("~/Features/Blocks/Fields/Heading/HeadingBlock.cshtml", currentContent));
        }
    }
}
