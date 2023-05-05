using Microsoft.AspNetCore.Mvc;

using Perficient.Web.Features.Navigation.Services;

namespace Perficient.Web.Features.Navigation.Controllers
{
    [ApiController]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/navigation")]
    [Route("api/navigation")]
    public class NavigationApiController : ControllerBase
    {
        private readonly INavigationService _navigationService;
        public NavigationApiController(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [HttpGet]
        [Route("footer")]
        public IActionResult Footer()
        {
            var footer = _navigationService.GetFooter(new System.Globalization.CultureInfo("en"));

            return new OkObjectResult(footer);
        }

        [HttpGet]
        [Route("header")]
        public IActionResult Header()
        {
            var header = _navigationService.GetHeader(new System.Globalization.CultureInfo("en"));

            return new OkObjectResult(header);
        }
    }
}
