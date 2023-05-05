using EPiServer.Shell.Services.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Perficient.Web.Features.ContentTypeReport;
using Perficient.Web.Features.ContentTypeReport.Models;
using Perficient.Web.Features.ContentTypeReport.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.ContentTypeReport.Controllers
{
    [Route("Admin/LegacyContentTypeReport/ContentReferencesReport", Name = "LegacyContentReferencesReportRoot")]
    [Authorize]
    public class LegacyContentReferencesReportController : Controller
    {
        private readonly IContentTypeReportService _contentTypeReportService;

        public LegacyContentReferencesReportController(IContentTypeReportService contentTypeReportService)
        {
            _contentTypeReportService = contentTypeReportService;
        }

        [HttpGet]
        [Route("", Name = "lcf_Root")]
        public ActionResult Index(int Id, string ContentName)
        {
            var contentReferencesSummaryViewModel = new ContentReferencesSummaryViewModel();
            List<InstancesSummaryModel> instanceSummary = _contentTypeReportService.GetInstancesOfContent(Id);

            contentReferencesSummaryViewModel.instancesSummary = instanceSummary;
            contentReferencesSummaryViewModel.ContentId = Id;
            contentReferencesSummaryViewModel.ContentName = ContentName;
            return View("/Features/ContentTypeReport/Views/LegacyContentReferences/Index.cshtml", contentReferencesSummaryViewModel);
        }
    }
}
