using Perficient.Web.Features.ContentTypeReport.Helpers;
using Perficient.Web.Features.ContentTypeReport.Models;

namespace Perficient.Web.Features.ContentTypeReport.ViewModels
{
    public class ContentDetailsReportViewModel
    {
        public ContentDetailsModel contentDetailsModel { get; set; } = new ContentDetailsModel();    

        public string PropertyDetailsHTMLString
        {
            get
            {
                return HTMLTableHelper.ToHtmlTable(contentDetailsModel.Properties);
            }          
        }
    }
}

