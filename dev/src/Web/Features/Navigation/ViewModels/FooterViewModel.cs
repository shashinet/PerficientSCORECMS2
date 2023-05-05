using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class FooterViewModel : BaseBlockViewModel
    {
        public FooterViewModel()
        {
            UpperFooter = new List<object>();
            LowerFooter = new LowerFooterViewModel();
            FooterStyle = new List<string>();
            GlobalStyle = new List<string>();
            ContentType = "Footer";
        }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("footerStyle")]
        public List<string> FooterStyle { get; set; }

        [JsonProperty("logoUrl")]
        public string LogoUrl { get; set; }

        [JsonProperty("image")]
        public object Image { get; set; }

        [JsonProperty("upperFooter")]
        public List<object> UpperFooter { get; set; }

        [JsonProperty("lowerFooter")]
        public LowerFooterViewModel LowerFooter { get; set; }

    }
}
