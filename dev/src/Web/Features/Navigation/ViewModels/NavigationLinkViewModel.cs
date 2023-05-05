using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class NavigationLinkViewModel : BaseBlockViewModel
    {
        public NavigationLinkViewModel()
        {
            ContentType = "NavigationLink";
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("openInNewWindow")]
        public bool OpenInNewWindow { get; set; }
    }
}
