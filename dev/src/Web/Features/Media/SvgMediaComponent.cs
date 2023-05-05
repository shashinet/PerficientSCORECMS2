using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Media
{
    public class SvgMediaComponent : AsyncPartialContentComponent<SvgMedia>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(SvgMedia currentBlock)
        {
            return await Task.FromResult(View("~/Features/Media/SvgMedia.cshtml", currentBlock));
        }
    }
}
