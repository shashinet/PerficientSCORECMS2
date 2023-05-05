using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Navigation.Models;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Navigation.Controllers
{
    public class UtilitySearchBlockComponent : AsyncPartialContentComponent<UtilitySearchBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(UtilitySearchBlock currentBlock)
        {
            return await Task.FromResult(View("~/Features/Navigation/Views/UtilitySearchBlock.cshtml", currentBlock));
        }
    }
}
