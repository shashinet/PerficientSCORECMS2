using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Models.ViewModels;
using Perficient.Infrastructure.Settings.Interfaces;

namespace Perficient.Web.Features.Pages.HttpStatus
{
    [Route("error")]
    public class HTTPStatusPageController : PageController<HttpStatusPage>
    {
        private readonly IContentRepository _contentRepository;
        private readonly ISettingsService _settingsService;

        public HTTPStatusPageController(ISettingsService settingsService, IContentRepository contentRepository)
        {
            _settingsService = settingsService;
            _contentRepository = contentRepository;
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {
            var errorPage = _settingsService.GetSiteSettings<Perficient.Infrastructure.Settings.Models.Content.SiteSettings>()?.PageNotFound;
            if (ContentReference.IsNullOrEmpty(errorPage))
            {
                return Content("Page Not Found");
            }

            var pageModel = _contentRepository.Get<HttpStatusPage>(errorPage);
            if (pageModel == null)
            {
                return Content("Page Not Found");
            }

            return View("HttpStatusPage", ContentViewModel.Create(pageModel));
        }

        [Route("500")]
        public IActionResult InternalServerError()
        {
            return File("~/500.html", "text/html");
        }
    }
}
