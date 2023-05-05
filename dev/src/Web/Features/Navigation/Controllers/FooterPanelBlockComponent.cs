using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Navigation.Models;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Navigation.Controllers
{
    public class FooterPanelBlockComponent : AsyncPartialContentComponent<NavigationPanelBlock>
    {
        public FooterPanelBlockComponent() { }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(NavigationPanelBlock currentBlock)
        {
            return await Task.FromResult(View("~/Features/Navigation/Views/FooterPanelBlock.cshtml", currentBlock));
        }
    }
}
