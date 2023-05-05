using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Navigation.Models;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Navigation.Controllers
{
    public class MegaMenuFlyoutBlockComponent : AsyncPartialContentComponent<MegaMenuFlyoutBlock>
    {
        public MegaMenuFlyoutBlockComponent() { }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(MegaMenuFlyoutBlock currentBlock)
        {
            return await Task.FromResult(View("~/Features/Navigation/Views/MegaMenuFlyoutBlock.cshtml", currentBlock));
        }
    }
}
