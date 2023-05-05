using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Components.Tile
{
    public class TileViewModel : BaseBlockViewModel
    {
        public TileViewModel()
        {
            TileStyles = new List<string>();
            ContentType = "Tile";
        }

        [JsonProperty("image")]
        public object Image { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("href")]
        public string Link { get; set; }

        [JsonProperty("tileStyles")]
        public List<string> TileStyles { get; set; }

        [JsonProperty("openInNewWindow")]
        public bool OpenInNewWindow { get; set; }
    }
}
