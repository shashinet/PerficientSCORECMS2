using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.Categories;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Web.Features.Categories;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Models
{

    [ContentType(GUID = "3ca91b37-52b3-40a8-a000-f9e8ea6ee125",
   DisplayName = "Article Category",
       Description = "Used to categorize article pages")]
    [ImageUrl("~/icons/score/epi_score128_category.png")]
    [AvailableContentTypes(IncludeOn = new[] { typeof(ArticleFolderCategory) }, ExcludeOn = new[]  { typeof(StandardCategory) })]
    public class ArticleCategory : CategoryData
    {
        [Display(GroupName = SystemTabNames.Content, Order = 20, Name = "Color")]
        [EditorDescriptor(EditorDescriptorType = typeof(ColorPickerEditorDescriptor))]
        [UIHint("ColorPickerEditor")]
        public virtual string Color { get; set; }
    }
}
