using EPiServer.DataAnnotations;
using Geta.Optimizely.Categories;

namespace Perficient.Web.Features.Categories
{

    [ContentType(GUID = "5CE3E82F-426C-412F-A35A-8BAF291425B3",
        DisplayName = "Standard Category",
        Description = "Used to categorize content")]
    [ImageUrl("~/icons/score/epi_score128_category.png")]
    public class StandardCategory : CategoryData
    {
      
    }
}
