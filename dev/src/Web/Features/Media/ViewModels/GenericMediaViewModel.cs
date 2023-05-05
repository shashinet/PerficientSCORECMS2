using Newtonsoft.Json;

namespace Perficient.Web.Features.Media.ViewModels
{
    public class GenericMediaViewModel
    {

        [JsonProperty("videoname")]
        public string VideoName { get; set; }

        [JsonProperty("contenttype")]
        public string ContentType { get; set; }

        [JsonProperty("videothumb")]
        public string VideoThumb { get; set; }

        [JsonProperty("videosrc")]
        public string VideoSrc { get; set; }
    }
}
