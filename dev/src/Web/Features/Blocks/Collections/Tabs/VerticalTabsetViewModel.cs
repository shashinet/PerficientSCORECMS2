using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Collections.Tabs
{
    public class VerticalTabsetViewModel : BaseBlockViewModel
    {
        public VerticalTabsetViewModel()
        {
            ContentType = "VerticalTabset";
            Panels = new List<object>();
            CallToActionButtons = new List<object>();
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("titleTag")]
        public string TitleTag { get; set; }

        [JsonProperty("subTitle")]
        public string SubTitle { get; set; }

        [JsonProperty("subTitleTag")]
        public string SubTitleTag { get; set; }

        [JsonProperty("panels")]
        public List<object> Panels { get; set; }

        [JsonProperty("callToActionButtons")]
        public List<object> CallToActionButtons { get; set; }

    }
}





