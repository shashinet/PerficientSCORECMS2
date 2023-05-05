using System;
using System.Collections.Generic;

namespace Perficient.Web.Features.ContentTypeReport.Models
{  
  
    public class InstancesSummaryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EditUrl { get; set; }
        public string PageUrl { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Author { get; set; }
        public int Count { get; set; }        
        public List<ReferencesSummaryModel> InstantReferences { get; set; }
    }

}
