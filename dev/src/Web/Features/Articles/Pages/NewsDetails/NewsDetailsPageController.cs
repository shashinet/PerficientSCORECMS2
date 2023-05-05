using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Models.ViewModels;

namespace Perficient.Web.Features.Articles.Pages.NewsDetails
{
    public class NewsDetailsPageController : PageController<NewsDetailsPage>
    {
        public ActionResult Index(NewsDetailsPage currentContent)
        {
            var model = new ContentViewModel<NewsDetailsPage>(currentContent);
            return View("~/Features/Articles/Pages/NewsDetails/NewsDetailsPage.cshtml", model);
        }      
    }
}
