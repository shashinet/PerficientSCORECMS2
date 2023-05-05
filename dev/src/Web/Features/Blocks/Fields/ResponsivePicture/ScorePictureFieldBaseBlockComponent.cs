using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    public class ScorePictureFieldBaseBlockComponent : AsyncBlockComponent<ScorePictureFieldBaseBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(ScorePictureFieldBaseBlock currentBlock)
        {
            return await Task.FromResult(View("~/Features/Blocks/Fields/ResponsivePicture/ScorePictureFieldBaseBlock.cshtml", currentBlock));
        }
    }
}
