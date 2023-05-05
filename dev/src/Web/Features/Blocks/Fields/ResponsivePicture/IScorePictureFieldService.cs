using EPiServer.Core;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    public interface IScorePictureFieldService
    {
        List<CropResult> CroppingsExistFor(CheckImage checkImage);

        ImageData Crop(CropperPost cropResult);

        SystemCroppingFolder GetCroppingFolder(ContentReference parentReference);

        string GenerateImageFileName(string originalImageName, string deviceName, string blockId);
    }
}
