using EPiServer.Web.Mvc;

using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Models.ViewModels;

namespace Perficient.Web.Features.Pages.Home
{
    public class HomePageController : PageController<HomePage>
    {
        public ActionResult Index(HomePage currentContent)
        {
            return View("HomePage",ContentViewModel.Create<HomePage>(currentContent));
        }
    }
}
