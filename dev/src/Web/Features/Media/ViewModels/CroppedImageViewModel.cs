using Newtonsoft.Json;

namespace Perficient.Web.Features.Media.ViewModels
{
    public class CroppedImageViewModel
    {
        public CroppedImageViewModel()
        {
            ContentType = "CroppedImage";
        }

        [JsonProperty("imageSrc")]
        public string ImageSrc { get; set; }

        [JsonProperty("srcSet")]
        public string SrcSet { get; set; }

        [JsonProperty("contentType")]
        public object ContentType { get; set; }

    }
}
