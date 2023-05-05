using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class SocialIconViewModel : BaseBlockViewModel
    {
        public SocialIconViewModel()
        {
            ContentType = "SocialIcon";
        }

        [JsonProperty("image")]
        public object Image { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("openInNewWindow")]
        public bool OpenInNewWindow { get; set; }

    }
}
