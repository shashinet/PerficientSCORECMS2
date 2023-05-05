using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Collections.Slider
{
    public class SliderViewModel : BaseBlockViewModel
    {
        public SliderViewModel()
        {
            ContentType = "Slider";
            Cards = new List<object>();
            CallToActionButtons = new List<object>();
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("titleTag")]
        public string TitleTag { get; set; }

        [JsonProperty("cards")]
        public List<object> Cards { get; set; }

        [JsonProperty("callToActionButtons")]
        public List<object> CallToActionButtons { get; set; }

        [JsonProperty("showPagination")]
        public bool ShowPagination { get; set; }
    }
}





