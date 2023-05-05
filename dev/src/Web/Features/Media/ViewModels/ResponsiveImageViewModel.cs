using Newtonsoft.Json;
using System.Collections.Generic;

namespace Perficient.Web.Features.Media.ViewModels
{
    public class ResponsiveImageViewModel
    {
        public ResponsiveImageViewModel()
        {
            Croppings = new List<object>();
            ContentType = "ResponsiveImage";
        }

        [JsonProperty("original")]
        public object Original { get; set; }

        [JsonProperty("croppings")]
        public List<object> Croppings { get; set; }

        [JsonProperty("contentType")]
        public object ContentType { get; set; }

    }
}
