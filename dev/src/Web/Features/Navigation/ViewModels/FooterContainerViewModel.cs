using Newtonsoft.Json;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class FooterContainerViewModel
    {
        public FooterContainerViewModel()
        {
            Footer = new FooterViewModel();
        }

        [JsonProperty("footer")]
        public FooterViewModel Footer { get; set; }
    }
}
