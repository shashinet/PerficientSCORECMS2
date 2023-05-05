using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;

namespace Perficient.Web.Features.Blocks.Components.Button
{
    public class ButtonViewModel : BaseBlockViewModel
    {

        public ButtonViewModel()
        {
            ContentType = "Button";
        }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("openInNewWindow")]
        public bool OpenInNewWindow { get; set; }
    }
}
