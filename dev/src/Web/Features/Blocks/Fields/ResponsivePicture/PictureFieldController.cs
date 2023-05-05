using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    [ApiController]
    [Route("api/picture")]
    public class PictureFieldController : ControllerBase
    {
        private readonly IScorePictureFieldService _scorePictureFieldService;

        public PictureFieldController(IScorePictureFieldService _scorePictureFieldService)
        {
            this._scorePictureFieldService = _scorePictureFieldService;
        }

        [Route("crop")]
        [HttpPost]
        [Authorize]
        public List<CropResult> Crop(List<CropperPost> results)
        {
            var retVal = new List<CropResult>();

            foreach (var cropResult in results)
            {
                var imageResult = _scorePictureFieldService.Crop(cropResult);

                retVal.Add(new CropResult
                {
                    Image = imageResult.ContentLink,
                    Device = cropResult.deviceSpec.device
                });
            }

            return retVal;
        }

        [Route("check")]
        [HttpPost]
        [Authorize]
        public List<CropResult> CheckCroppedImages(CheckImage checkImage)
        {
            return _scorePictureFieldService.CroppingsExistFor(checkImage);
        }

        [HttpGet]
        [Route("version")]
        public virtual string Version()
        {
            return "1.0";
        }
    }
}
