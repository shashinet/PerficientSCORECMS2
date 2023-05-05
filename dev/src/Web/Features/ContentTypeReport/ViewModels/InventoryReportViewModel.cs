using Microsoft.AspNetCore.Mvc.Rendering;
using Perficient.Web.Features.ContentTypeReport.Models;
using System;
using System.Collections.Generic;

namespace Perficient.Web.Features.ContentTypeReport.ViewModels
{
    public class InventoryReportViewModel
    {
        public List<SelectListItem> ContentTypeItems { get; set; }
        public string ContentDetailsHTMLString { get; set; }
        public IEnumerable<ContentBasicInformationModel> ContentTypes { get; set; }

    }
}

