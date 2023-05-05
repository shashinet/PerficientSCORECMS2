using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Models.Base;

namespace Perficient.Web.Features.Pages.HttpStatus
{
    [ContentType(
        DisplayName = "HTTP Status Page",
        GUID = "FE5DF3A0-3D91-4E8B-9EB6-00597E281D24",
        Description = "Instance of this page is rendered on any HTTP Errors",
        GroupName = TabNames.Default)]
    [ImageUrl("~/icons/score/epi_score128_error_page.png")]
    public class HttpStatusPage : BasePage
    {

    }
}
