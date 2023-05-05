using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Navigation.Models;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Navigation.Controllers
{
    public class MenuListBlockController : AsyncPartialContentComponent<MenuListBlock>
    {
        public MenuListBlockController() { }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(MenuListBlock currentContent)
        {
            return await Task.FromResult(View("~/Features/Navigation/Views/_MenuListBlock.cshtml", currentContent));
        }
    }
}
