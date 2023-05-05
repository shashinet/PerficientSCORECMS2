using EPiServer.Core;

namespace Perficient.Web.Features.Articles.Models
{
    public class CategoryListingModel
    {
        public ContentReference CurrentPage { get; set; }
        public ContentReference SelectedCategory { get; set; }
    }
}
