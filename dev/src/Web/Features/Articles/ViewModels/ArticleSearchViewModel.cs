using Perficient.Web.Features.Articles.Models.Enums;
using System.Text.Json.Serialization;

namespace Perficient.Web.Features.Articles.ViewModels
{
    public class ArticleSearchViewModel : ArticleSearchFilterViewModel
    {
        
        [JsonPropertyName("searchTextPlaceHolder")]
        public string SearchTextPlaceholder { get; set; } = "Search Articles";
        [JsonPropertyName("showSearch")]
        public bool ShowSearch { get; set; } = true;

        public ArticleSearchViewModel()
        {
        }
        public ArticleSearchViewModel(ArticleTypes articleType, int categoryId, int rootPageId, string searchTerm, bool showSearch, string trackId, string searchTextPlaceholder = null)
        {
            ArticleType = articleType;
            CategoryId = categoryId;
            RootPageId = rootPageId;
            SearchTerm = searchTerm;
            ShowSearch = showSearch;
            TrackId = trackId;
            if (TrackId == null || TrackId.Trim() == string.Empty) Track = false;
            if (searchTextPlaceholder != null) SearchTextPlaceholder = searchTextPlaceholder;
        }
    }
}
