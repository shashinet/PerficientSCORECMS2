using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class RichTextViewModel : BaseBlockViewModel
    {
        public RichTextViewModel()
        {
            ContentType = "RichText";
        }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
