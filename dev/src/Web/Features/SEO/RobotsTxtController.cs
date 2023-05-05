using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Settings.Interfaces;
using System.Text;

namespace Perficient.Web.Features.SEO
{
    public class RobotsTxtController : Controller
    {
        private readonly ISettingsService _settingsService;

        public RobotsTxtController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [Route("robots.txt")]
        public ActionResult Index()
        {
            var robotsContent = _settingsService.GetSiteSettings<Perficient.Infrastructure.Settings.Models.Content.SiteSettings>()?.RobotsText;
            return Content(robotsContent, "text/plain", Encoding.UTF8);
        }
    }
}
