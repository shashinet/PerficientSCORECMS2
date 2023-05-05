using Newtonsoft.Json;

namespace Perficient.Web.Features.Media.ViewModels
{
    public class ImageMediaViewModel
    {
        public ImageMediaViewModel(string src = "", string altText = "")
        {
            ImageSrc = src;
            AltText = altText;
            ContentType = "Image";
        }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("imageSrc")]
        public string ImageSrc { get; set; }

        [JsonProperty("altText")]
        public string AltText { get; set; }

    }
}
