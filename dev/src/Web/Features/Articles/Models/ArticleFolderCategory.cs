using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.Categories;
using Perficient.Web.Features.Categories;

namespace Perficient.Web.Features.Articles.Models
{
    [ContentType(GUID = "FBBE7C6E-1343-44B2-9F90-6CBE2D3DDBE6",
    DisplayName = "Article Folder Category",
        Description = "Used to categorize a collection of article pages.")]
    [ImageUrl("~/icons/score/epi_score128_category.png")]
    [AvailableContentTypes(IncludeOn = new[] { typeof(StandardCategory) }, ExcludeOn = new[] { typeof(StandardCategory) })]
    public class ArticleFolderCategory : CategoryData
    {
        

    }    
}
