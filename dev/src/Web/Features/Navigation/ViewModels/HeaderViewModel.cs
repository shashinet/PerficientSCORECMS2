using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class HeaderViewModel : BaseBlockViewModel
    {
        public HeaderViewModel()
        {
            NavigationItems = new List<object>();
            UtilityNavigation = new List<object>();
            HeaderStyle = new List<string>();
            GlobalStyle = new List<string>();
            ContentType = "Header";
        }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("headerStyle")]
        public List<string> HeaderStyle { get; set; }              

        [JsonProperty("image")]
        public object Image { get; set; }               

        [JsonProperty("tagline")]
        public string TagLine { get; set; }

        [JsonProperty("navigationItems")]
        public List<object> NavigationItems { get; set; }

        [JsonProperty("utilityNavigation")]
        public List<object> UtilityNavigation { get; set; }
    }
}
