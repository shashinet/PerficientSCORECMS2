using Newtonsoft.Json;

namespace Perficient.Web.Features.Media.ViewModels
{
    public class SvgMediaViewModel
    {
        public SvgMediaViewModel(string src = "", string altText = "")
        {
            ImageSrc = src;
            AltText = altText;
            ContentType = "Svg";
        }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("imageSrc")]
        public string ImageSrc { get; set; }

        [JsonProperty("altText")]
        public string AltText { get; set; }

    }
}
