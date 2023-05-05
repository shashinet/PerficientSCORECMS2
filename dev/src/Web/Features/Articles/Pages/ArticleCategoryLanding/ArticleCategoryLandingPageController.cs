using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Models.ViewModels;

namespace Perficient.Web.Features.Articles.Pages.ArticleCategoryLanding
{
    public class ArticleCategoryLandingPageController : PageController<ArticleCategoryLandingPage>
    {
        public ActionResult Index(ArticleCategoryLandingPage currentContent)
        {
            var model = new ContentViewModel<ArticleCategoryLandingPage>(currentContent);
            return View("~/Features/Articles/Pages/ArticleCategoryLanding/ArticleCategoryLandingPage.cshtml", model);
        }
    }
}
