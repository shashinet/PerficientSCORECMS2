using Newtonsoft.Json;
using System.Collections.Generic;

namespace Perficient.Infrastructure.Models.ViewModels
{
    public class BaseBlockViewModel
    {
        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayOption")]
        public string DisplayOption { get; set; }

        [JsonProperty("globalStyle")]
        public List<string> GlobalStyle { get; set; }

    }
}
