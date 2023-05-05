using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Models.ViewModels;
using Perficient.Web.Features.Articles.Pages.BlogDetails;

namespace Perficient.Web.Features.Articles
{
    public class BlogDetailsPageController : PageController<BlogDetailsPage>
    {
        public ActionResult Index(BlogDetailsPage currentContent)
        {
            var model = new ContentViewModel<BlogDetailsPage>(currentContent);
            return View("~/Features/Articles/Pages/BlogDetails/BlogDetailsPage.cshtml", model);
        }
    }
}
