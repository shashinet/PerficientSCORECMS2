using Newtonsoft.Json;
using System.Collections.Generic;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class LowerFooterViewModel
    {
        public LowerFooterViewModel()
        {
            SocialIcons = new List<object>();
            LowerFooterContent = new List<object>();
        }

        [JsonProperty("socialIcons")]
        public List<object> SocialIcons { get; set; }

        [JsonProperty("lowerFooterContent")]
        public List<object> LowerFooterContent { get; set; }
    }
}
