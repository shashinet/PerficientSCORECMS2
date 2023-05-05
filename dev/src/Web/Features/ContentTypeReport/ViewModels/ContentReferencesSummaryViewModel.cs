using Perficient.Web.Features.ContentTypeReport.Helpers;
using Perficient.Web.Features.ContentTypeReport.Models;
using System;
using System.Collections.Generic;

namespace Perficient.Web.Features.ContentTypeReport.ViewModels
{
    public class ContentReferencesSummaryViewModel
    {
        public int ContentId { get; set; }
       
        public String ContentName { get; set; }
        
        public string ContentReferencesHTMLString
        {
            get
            {

                contentInstancesSummary.AddRange(instancesSummary.ConvertAll(x => new ContentInstancesSummaryViewModel() {
                    Id = x.Id,
                    Name = x.Name,
                    EditUrl = x.EditUrl,
                    PageUrl = x.PageUrl,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Author = x.Author,
                    Count = x.Count,
                    PageReferences = x.Count == 0 ? String.Empty :"<a class='ex1' id='link" + x.Id + "' href=javascript:ShowRef(link" + x.Id + ",tr" + x.Id + ")>Show References</a>",
                    InstantReferences  = x.InstantReferences.Count == 0 ? null : HTMLTableHelper.ToHtmlTable(x.InstantReferences)
                }));

                return HTMLTableHelper.ToHtmlTableAndSubTable(contentInstancesSummary);
            }
        }

        public List<InstancesSummaryModel> instancesSummary { get; set; }
        public List<ContentInstancesSummaryViewModel> contentInstancesSummary { get; set; }=new List<ContentInstancesSummaryViewModel>();
    }
}
