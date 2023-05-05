using Perficient.Web.Features.Articles.Models.Enums;
using System.Text.Json.Serialization;

namespace Perficient.Web.Features.Articles.ViewModels
{
    public class ArticleSearchFilterViewModel
    {
        [JsonPropertyName("articleType")]
        public ArticleTypes ArticleType { get; set; }
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; } = 1;
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 10;
        [JsonPropertyName("rootPageId")]
        public int RootPageId { get; set; }
        [JsonPropertyName("searchTerm")]
        public string SearchTerm { get; set; }
        [JsonPropertyName("sort")]
        public ArticleSortBy Sort { get; set; }
        [JsonPropertyName("track")]
        public bool Track { get; set; } = true;
        [JsonPropertyName("trackId")]
        public string TrackId { get; set; }
    }
}

