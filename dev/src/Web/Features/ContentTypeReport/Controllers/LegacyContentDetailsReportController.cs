using EPiServer.Shell.Services.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Perficient.Web.Features.ContentTypeReport;
using Perficient.Web.Features.ContentTypeReport.ViewModels;
namespace Perficient.Web.Features.ContentTypeReport.Controllers
{
    [Route("Admin/LegacyContentTypeReport/ContentDetailsReport", Name = "LegacyContentDetailsReportRoot")]
    [Authorize]
    public class LegacyContentDetailsReportController : Controller
    {
        private readonly IContentTypeReportService _contentTypeReportService;

        public LegacyContentDetailsReportController(IContentTypeReportService contentTypeReportService)
        {
            _contentTypeReportService = contentTypeReportService;
        }

        [HttpGet("", Name = "lcd_Root")]
        public ActionResult Index(int Id, string ErrorMessage)
        {
            var contentDetailsReportViewModel = new ContentDetailsReportViewModel();
            if (Id > 0) { contentDetailsReportViewModel = LoadContentDetailsModel(Id); }
            contentDetailsReportViewModel.contentDetailsModel.ErrorMessage = ErrorMessage??"";
            return View("/Features/ContentTypeReport/Views/LegacyContentDetails/Index.cshtml", contentDetailsReportViewModel);
        }

        private ContentDetailsReportViewModel LoadContentDetailsModel(int Id)
        {
            var contentDetailsReportViewModel = new ContentDetailsReportViewModel();
            contentDetailsReportViewModel.contentDetailsModel = _contentTypeReportService.GetProperties(Id);
            return contentDetailsReportViewModel;
        }

        [HttpPost("AddEditUsage", Name = "lcd_AddEditUsage")]
        public ActionResult AddEditUsage(int Id)
        {
            var contentDetailsReportViewModel = LoadContentDetailsModel(Id);
            contentDetailsReportViewModel.contentDetailsModel.EditMode = true;
            return View("/Features/ContentTypeReport/Views/LegacyContentDetails/Index.cshtml", contentDetailsReportViewModel);
        }

        [HttpPost("SaveUsage", Name = "lcd_SaveUsage")]
        public ActionResult SaveUsage(int ContentID, string UseWhen, string DoNotUseWhen)
        {
            _contentTypeReportService.SaveUsageDetails(ContentID, UseWhen, DoNotUseWhen);
            var contentDetailsReportViewModel = LoadContentDetailsModel(ContentID);
            return View("/Features/ContentTypeReport/Views/LegacyContentDetails/Index.cshtml", contentDetailsReportViewModel);
        }
    }
}
