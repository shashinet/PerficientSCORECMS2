using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class UtilitySearchBoxViewModel : BaseBlockViewModel
    {
        public UtilitySearchBoxViewModel()
        {
            ContentType = "SearchButton";
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("placeholderText")]
        public string PlaceholderText { get; set; }

        [JsonProperty("searchPage")]
        public string SearchPage { get; set; }

    }
}
