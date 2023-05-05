using System;

namespace Perficient.Web.Features.ContentTypeReport.ViewModels
{
    public class ContentInstancesSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EditUrl { get; set; }
        public string PageUrl { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Author { get; set; }
        public int Count { get; set; }
        public String PageReferences { get; set; }
        public String InstantReferences { get; set; }
    }
}
