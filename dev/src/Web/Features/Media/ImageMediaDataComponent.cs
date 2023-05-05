using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Media
{
    public class ImageMediaDataComponent : AsyncPartialContentComponent<ImageMediaData>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(ImageMediaData currentBlock)
        {
            return await Task.FromResult(View("~/Features/Media/ImageMediaData.cshtml", currentBlock));
        }
    }
}
