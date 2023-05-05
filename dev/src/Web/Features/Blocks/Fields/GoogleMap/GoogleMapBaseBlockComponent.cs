using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Fields.GoogleMap
{
    public class GoogleMapBaseBlockComponent : AsyncBlockComponent<GoogleMapBaseBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(GoogleMapBaseBlock currentBlock)
        {
            return await Task.FromResult(View("~/Features/Blocks/Fields/GoogleMap/GoogleMapBaseBlock.cshtml", currentBlock));
        }
    }
}
