using System;
using System.Collections.Generic;

namespace Perficient.Web.Features.ContentTypeReport.Models
{   
    public class ContentDetailsModel
    {
        public int ContentID { get; set; }
        public Guid GUID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public string AvailableInEditMode { get; set; }
        public string IconPath { get; set; }
        public string IncludeContentTypes { get; set; }
        public string ExcludeContentTypes { get; set; }
        public bool EditMode { get; set; }
        public string ErrorMessage { get; set; }
        public ContentUsageDetail UsageDetails { get; set; } = new ContentUsageDetail();
        public IList<ContentUsageImage> UsageImages { get; set; } = new List<ContentUsageImage>();
        public List<ContentTypePropertiesDetailsModel> Properties { get; set; } = new List<ContentTypePropertiesDetailsModel>();
    }
}
