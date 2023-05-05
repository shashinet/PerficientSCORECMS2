using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Components.Card

{
    public class CardViewModel : BaseBlockViewModel
    {
        public CardViewModel()
        {
            ContentType = "Card";
            CallToAction = new List<object>();
        }

        [JsonProperty("cardStyle")]
        public List<string> CardStyle { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("titleTag")]
        public string TitleTag { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("imageText")]
        public string ImageText { get; set; }

        [JsonProperty("bodyDescription")]
        public string BodyDescription { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("callToAction")]
        public List<object> CallToAction { get; set; }
    }
}





