using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Models.ViewModels;

namespace Perficient.Web.Features.Articles.Pages.BlogHome
{
    public class ArticleHomePageController : PageController<ArticleHomePage>
    {
        public ActionResult Index(ArticleHomePage currentContent)
        {
            var model = new ContentViewModel<ArticleHomePage>(currentContent);
            return View("~/Features/Articles/Pages/ArticleHome/ArticleHomePage.cshtml", model);
        }
    }
}
