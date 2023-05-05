using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Collections.Tabs
{
    public class TabPanelViewModel : BaseBlockViewModel
    {
        public TabPanelViewModel()
        {
            ContentType = "TabPanel";
            CallToActionButtons = new List<object>();
        }

        [JsonProperty("buttonText")]
        public string ButtonText { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("imageText")]
        public string ImageText { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("callToActionButtons")]
        public List<object> CallToActionButtons { get; set; }

    }
}





