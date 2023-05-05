using Perficient.Web.Features.ContentTypeReport.Controllers;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perficient.Web.UnitTests.Features.ContentTypeReport;
using FakeItEasy;
using Perficient.Web.Features.ContentTypeReport;
using Perficient.Web.Features.ContentTypeReport.Models;

namespace Perficient.Web.UnitTests.Features.ContentTypeReport
{
    public class ContentTypeReportControllerTests
    {
        [Theory]
        [JsonFileData("..//..//..//Features//ContentTypeReport//Data//ContentTypeReportResultsData.json", "GetContentTypeOptions", typeof(IList<string>))]
        public void GetContentTypeOptionsTest(List<string> expectedResult)
        {
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.GetContentTypeOptions()).Returns(expectedResult);

            Assert.IsAssignableFrom<IList<string>>(expectedResult);
            Assert.True(expectedResult.Any());
            Assert.True(expectedResult.Count > 0);
        }

        [Theory]
        [JsonFileData("..//..//..//Features//ContentTypeReport//Data//ContentTypeReportResultsData.json", "EmptyGetContentTypeOptions", typeof(IList<string>))]
        public void GetContentTypeOptionsTest_Empty(List<string> expectedResult)
        {
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.GetContentTypeOptions()).Returns(expectedResult);

            Assert.IsAssignableFrom<IList<string>>(expectedResult);
            Assert.True(expectedResult.Count == 0);
        }

        [Theory]
        [JsonFileData("..//..//..//Features//ContentTypeReport//Data//ContentTypeReportResultsData.json", "GetContentTypes", typeof(string), typeof(IList<ContentBasicInformationModel>))]
        public void GetContentTypesTest(string ContentType, List<ContentBasicInformationModel> expectedResult)
        {
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.GetContentTypes(ContentType)).Returns(expectedResult);

            Assert.IsAssignableFrom<IList<ContentBasicInformationModel>>(expectedResult);
            Assert.True(expectedResult.Any());
            Assert.True(expectedResult.Count > 0);
        }

        [Theory]
        [JsonFileData("..//..//..//Features//ContentTypeReport//Data//ContentTypeReportResultsData.json", "EmptyGetContentTypes", typeof(string), typeof(IList<ContentBasicInformationModel>))]
        public void GetContentTypesTest_Empty(string ContentType, List<ContentBasicInformationModel> expectedResult)
        {
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.GetContentTypes(ContentType)).Returns(expectedResult);

            Assert.IsAssignableFrom<IList<ContentBasicInformationModel>>(expectedResult);
            Assert.True(expectedResult.Count == 0);
        }


        [Theory()]
        [JsonFileData("..//..//..//Features//ContentTypeReport//Data//ContentTypeReportResultsData.json", "GetProperties", typeof(int), typeof(ContentDetailsModel))]
        public void GetPropertiesTest(int ContentId, ContentDetailsModel expectedResult)
        {
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.GetProperties(ContentId)).Returns(expectedResult);

            Assert.IsAssignableFrom<ContentDetailsModel>(expectedResult);
            Assert.True(expectedResult.ContentID != 0);            
        }

        [Theory]
        [JsonFileData("..//..//..//Features//ContentTypeReport//Data//ContentTypeReportResultsData.json", "EmptyGetProperties", typeof(int), typeof(ContentDetailsModel))]
        public void GetPropertiesTest_Empty(int ContentId, ContentDetailsModel expectedResult)
        {
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.GetProperties(ContentId)).Returns(expectedResult);

            Assert.IsAssignableFrom<ContentDetailsModel>(expectedResult);
            Assert.True(expectedResult.ContentID == 0);
        }

        [Theory()]
        [JsonFileData("..//..//..//Features//ContentTypeReport//Data//ContentTypeReportResultsData.json", "GetInstancesOfContent", typeof(int), typeof(IList<InstancesSummaryModel>))]
        public void GetInstancesOfContentTest(int ContentId, List<InstancesSummaryModel> expectedResult)
        {
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.GetInstancesOfContent(ContentId)).Returns(expectedResult);

            Assert.IsAssignableFrom<IList<InstancesSummaryModel>>(expectedResult);
            Assert.True(expectedResult.Any());
            Assert.True(expectedResult.Count > 0);
        }

        [Theory]
        [JsonFileData("..//..//..//Features//ContentTypeReport//Data//ContentTypeReportResultsData.json", "EmptyGetInstancesOfContent", typeof(int), typeof(IList<InstancesSummaryModel>))]
        public void GetInstancesOfContentTest_Empty(int ContentId, List<InstancesSummaryModel> expectedResult)
        {
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.GetInstancesOfContent(ContentId)).Returns(expectedResult);

            Assert.IsAssignableFrom<IList<InstancesSummaryModel>>(expectedResult);
            Assert.True(expectedResult.Count == 0);
        }


        [Fact()]
        public void SaveUsageTestSuccess()
        {
            
            int ContentID = 74;
            string UseWhen = "Use When Details";
            string DoNotUseWhen = "Do Not Use When Details";
            bool expectedResult = true;
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.SaveUsageDetails(ContentID, UseWhen, DoNotUseWhen)).Returns(expectedResult);

            Assert.True(expectedResult);
        }

        [Fact()]
        public void SaveUsageTestFailure()
        {
            int ContentID = 74;
            string UseWhen = "Use When Details";
            string DoNotUseWhen = "Do Not Use When Details";
            bool expectedResult = false;
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.SaveUsageDetails(ContentID, UseWhen, DoNotUseWhen)).Returns(expectedResult);

            Assert.False(expectedResult);
        }

        [Fact()]
        public void UpdateUsageTestSuccess()
        {
            int ContentID = 74;
            string UseWhen = "Use When Details";
            string DoNotUseWhen = "Do Not Use When Details";
            bool expectedResult = true;
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.SaveUsageDetails(ContentID, UseWhen, DoNotUseWhen)).Returns(expectedResult);

            Assert.True(expectedResult);
        }

        [Fact()]
        public void UpdateUsageTestFailure()
        {
            int ContentID = 74;
            string UseWhen = "Use When Details";
            string DoNotUseWhen = "Do Not Use When Details";
            bool expectedResult = false;
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.SaveUsageDetails(ContentID, UseWhen, DoNotUseWhen)).Returns(expectedResult);

            Assert.False(expectedResult);
        }

        [Fact()]
        public void DeleteUsageTest()
        {          
            int ContentID = 74;
            bool expectedResult = true;
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.DeleteUsageDetails(ContentID)).Returns(expectedResult);
            Assert.True(expectedResult);
        }

        [Fact()]
        public void DeleteUsageTestFailure()
        {
            int ContentID = 74;
            bool expectedResult = false;
            var _searchService = A.Fake<IContentTypeReportService>();
            _ = A.CallTo(() => _searchService.DeleteUsageDetails(ContentID)).Returns(expectedResult);
            Assert.False(expectedResult);
        }
    }
}