using EPiServer.Shell.Services.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Perficient.Web.Features.ContentTypeReport.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Admin/contentTypeReport", Name = "ContentTypeReportRoot")]
    public class ContentTypeReportController : Controller
    {
        private readonly IContentTypeReportService _contentTypeReportService;

        public ContentTypeReportController(IContentTypeReportService contentTypeReportService)
        {
            _contentTypeReportService = contentTypeReportService;
        }

        [HttpGet]
        [Route("getContentTypeOptions", Name = "ctr_GetTypeOptions")]
        [Authorize]
        public ActionResult GetContentTypeOptions()
        {
            return new RestResult { Data = _contentTypeReportService.GetContentTypeOptions() };
        }

        /// <summary>
        /// Content Properties
        /// </summary>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getContentTypes/{ContentType}", Name = "ctr_GetTypes")]
        [Authorize]
        public ActionResult GetContentTypes([FromRoute] string ContentType)
        {
            return new RestResult { Data = _contentTypeReportService.GetContentTypes(ContentType) };
        }

        /// <summary>
        /// Field Details
        /// </summary>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getProperties/{id:int}", Name = "ctr_GetProperties")]
        [Authorize]
        public ActionResult GetProperties([FromRoute] int Id)
        {
            return new RestResult { Data = _contentTypeReportService.GetProperties(Id) };
        }

        /// <summary>
        /// Instance of Pages
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getInstancesOfContent/{id:int}", Name = "ctr_GetInstances")]
        [Authorize]
        public ActionResult GetInstancesOfContent([FromRoute] int Id)
        {
            return new RestResult { Data = _contentTypeReportService.GetInstancesOfContent(Id) };
        }

        [Route("saveUsageDetails/{ContentID}/{UseWhen}/{DoNotUseWhen}", Name = "ctr_SaveDetails")]
        [HttpPost]
        [Authorize]
        public IActionResult SaveUsageDetails([FromRoute] int ContentId, [FromRoute] string UseWhen, [FromRoute] string DoNotUseWhen)
        {
            var IsSuccess = _contentTypeReportService.SaveUsageDetails(ContentId, UseWhen, DoNotUseWhen);
            if (!IsSuccess)
            {
                return BadRequest(new { ContentId, IsSuccess });
            }
            else
            {
                return Ok(new { ContentId, IsSuccess });
            }
        }

        [Route("deleteUsageDetails/{ContentId}", Name = "ctr_DeleteDetails")]
        [HttpPost]
        [Authorize]
        public IActionResult DeleteUsageDetails([FromRoute] int ContentId)
        {
            var IsSuccess = _contentTypeReportService.DeleteUsageDetails(ContentId);
            if (!IsSuccess)
            {
                return BadRequest(new { ContentId, IsSuccess });
            }
            else
            {
                return Ok(new { ContentId, IsSuccess });
            }
        }

        [Route("saveUsageImages/{ContentID}/{ImageName}", Name = "ctr_SaveImages")]
        [HttpPost]
        [Authorize]
        public IActionResult SaveUsageImages([FromRoute] int ContentId, [FromRoute] string ImageName)
        {
            var IsSuccess = _contentTypeReportService.SaveUsageImages(ContentId, ImageName);
            if (!IsSuccess)
            {
                return BadRequest(new { ContentId, IsSuccess });
            }
            else
            {
                return Ok(new { ContentId, IsSuccess });
            }
        }

        [Route("deleteUsageImages/{ContentID}/{ImageName}", Name = "ctr_DeleteImages")]
        [HttpPost]
        [Authorize]
        public IActionResult DeleteUsageImages([FromRoute] int ContentId, [FromRoute] string ImageName)
        {
            var IsSuccess = _contentTypeReportService.DeleteUsageImages(ContentId, ImageName);
            if (!IsSuccess)
            {
                return BadRequest(new { ContentId, IsSuccess });
            }
            else
            {
                return Ok(new { ContentId, IsSuccess });
            }
        }

        //****************************************************************
        //****  Edit / Update the following code, once we have FE ***
        //***** Pending Usage Images integration with azure and database
        //****************************************************************


        //Fix the code once FE is ready
        //[HttpPost]
        //[Authorize(Roles = "WebEditors")]
        //public async Task<ActionResult> UploadFile(HttpPostedFileBase file, int Id, int ImageCount)
        //{
        //    string imageUrl = string.Empty;
        //    if (ImageCount == 3)
        //    {
        //        return RedirectToAction("index", new { Id, ErrorMessage = imageLimitationValidationMsg });
        //    }
        //    if (file == null || file.ContentLength == 0)
        //    {
        //        return RedirectToAction("index", new { Id, ErrorMessage = imageUploadError });
        //    }
        //    try
        //    {
        //        imageUrl = await UploadToAzureBlob(file, Id);

        //        if (!string.IsNullOrEmpty(imageUrl))
        //        {
        //            var usageDetail = new EFDataRepository<ContentUsageImages>(_context);
        //            usageDetail.Add(new ContentUsageImages
        //            {
        //                ContentID = Id,
        //                UsageImage = imageUrl.ToString()
        //            });
        //            usageDetail.SaveChanges();
        //        }
        //        return RedirectToAction("index", new { Id });
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("index", new { Id, ErrorMessage = ex.Message.ToString() });
        //    }
        //}

        //private async Task<string> UploadToAzureBlob(HttpPostedFileBase file, int Id)
        //{
        //    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["EPiServerAzureBlobs"].ConnectionString);
        //    CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        //    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("usageimage");
        //    if (await cloudBlobContainer.CreateIfNotExistsAsync())
        //    {
        //        await cloudBlobContainer.SetPermissionsAsync(
        //            new BlobContainerPermissions
        //            {
        //                PublicAccess = BlobContainerPublicAccessType.Blob
        //            }
        //            );
        //    }
        //    string imageName = Guid.NewGuid().ToString() + "-" + Id + Path.GetExtension(file.FileName);
        //    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);
        //    await cloudBlockBlob.DeleteIfExistsAsync();
        //    cloudBlockBlob.Properties.ContentType = file.ContentType;
        //    await cloudBlockBlob.UploadFromStreamAsync(file.InputStream);
        //    return cloudBlockBlob.Uri.ToString();
        //}       
    }
}
