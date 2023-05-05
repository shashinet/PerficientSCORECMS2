using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class MegaMenuFlyoutViewModel : BaseBlockViewModel
    {
        public MegaMenuFlyoutViewModel()
        {
            NavigationPanels = new List<object>();
            CallToActionButtons = new List<object>();
            ContentType = "MegaMenuFlyout";
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("navigationPanels")]
        public List<object> NavigationPanels { get; set; }

        [JsonProperty("callToActionButtons")]
        public List<object> CallToActionButtons { get; set; }
    }
}

