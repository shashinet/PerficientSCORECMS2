using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Perficient.Web.Features.ContentTypeReport.Helpers;
using Perficient.Web.Features.ContentTypeReport.ViewModels;

namespace Perficient.Web.Features.ContentTypeReport.Controllers
{
    [Route("Admin/LegacyContentTypeReport")]
    [Authorize]
    public class LegacyContentTypeReportController : Controller
    {
        private readonly IContentTypeReportService _contentTypeReportService;

        public LegacyContentTypeReportController(IContentTypeReportService contentTypeReportService)
        {
            _contentTypeReportService = contentTypeReportService;
        }

        [HttpGet("", Name = "lct_Root")]
        public ActionResult Index()
        {
            return ContentTypeChosen();
        }

        [HttpPost("ContentTypeChosen", Name = "lct_ContentTypeChosen")]
        public ActionResult ContentTypeChosen(string ContentType = "Page")
        {
            var inventoryReportModel = new InventoryReportViewModel();
            inventoryReportModel.ContentTypeItems = _contentTypeReportService.GetContentTypeOptions().ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.ToString(),
                    Value = a.ToString(),
                    Selected = a.ToString() == ContentType ? true : false
                };
            });
            var contentTypes = _contentTypeReportService.GetContentTypes(ContentType);
            //contentTypes.ForEach(z => z.References = "<a class='ex1' href='/Episerver/Admin/LegacyContentReferencesReport?Id=" + z.Id + "&ContentName=" + z.Name + "'>References</a>");
            //contentTypes.ForEach(z => z.Name = "<a class='ex1' href='/Episerver/Admin/LegacyContentDetailsReport?Id=" + z.Id + "'>" + z.Name + "</a>");
            inventoryReportModel.ContentTypes = contentTypes;
            //inventoryReportModel.ContentDetailsHTMLString = HTMLTableHelper.ToHtmlTable(contentTypes);
            return View("/Features/ContentTypeReport/Views/LegacyContentType/Index.cshtml", inventoryReportModel);
        }
    }
}
