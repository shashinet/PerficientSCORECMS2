using Perficient.Web.Features.ContentTypeReport.Models;
using System.Collections.Generic;

namespace Perficient.Web.Features.ContentTypeReport
{
    public interface IContentTypeReportService
    {
        List<string> GetContentTypeOptions();
        List<ContentBasicInformationModel> GetContentTypes(string ContentType);
        ContentDetailsModel GetProperties(int ContentId);
        List<InstancesSummaryModel> GetInstancesOfContent(int ContentId);
        bool SaveUsageDetails(int ContentId, string UseWhen, string DoNotUseWhen);
        bool DeleteUsageDetails(int ContentId);
        bool SaveUsageImages(int ContentId, string ImageName);
        bool DeleteUsageImages(int ContentId, string ImageName);
    }
}


