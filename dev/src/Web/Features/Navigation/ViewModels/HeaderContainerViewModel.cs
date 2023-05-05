using Newtonsoft.Json;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class HeaderContainerViewModel
    {
        public HeaderContainerViewModel()
        {
            Header = new HeaderViewModel();
        }

        [JsonProperty("header")]
        public HeaderViewModel Header { get; set; }
    }
}
