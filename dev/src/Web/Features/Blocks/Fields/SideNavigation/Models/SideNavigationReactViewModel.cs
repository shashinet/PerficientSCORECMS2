using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.Models
{
    public class SideNavigationReactViewModel : BaseBlockViewModel
    {
        public SideNavigationReactViewModel()
        {
            NavigationItems = new List<SideNavigationItems>();
            SecondaryNavStyle = "default";
            ContentType = "Secondary Navigation";
            ButtonText = "Back To Top";
            ButtonStyle = "default";
        }

        [JsonProperty("secondaryNavStyle")]
        public string SecondaryNavStyle { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("navigationItems")]
        public List<SideNavigationItems> NavigationItems { get; set; }

        [JsonProperty("buttonText")]
        public string ButtonText { get; set; }

        [JsonProperty("buttonStyle")]
        public string ButtonStyle { get; set; }


    }

    public class SideNavigationItems
    {
        public SideNavigationItems()
        {
            
        }
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("openInNewWindow")]
        public bool OpenInNewWindow { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("childPages")]
        public List<SideNavigationItems> ChildPages { get; set; }
    }
}
