using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.Categories;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Articles.Blocks.ArticleCategoryLink;
using Perficient.Web.Features.Articles.Blocks.ArticlesTopPosts;
using Perficient.Web.Features.Articles.Models;
using Perficient.Web.Features.Articles.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles
{
    [ContentType(
         GroupName = GroupNames.Articles,
         DisplayName = "Articles Home Page",
         GUID = "3eec064b-1220-4724-972e-9fd7b2ccaf05",
         Description = "Landing Page for the top level of a section of articles.")]
    [ImageUrl("~/icons/score/epi_score128_page_1col.png")]
    public class ArticleHomePage : BasePage
    {
        [Display(Name = "Root Category",
            Description = "The category folder that contains the categories relevant to this section of the site.",
            GroupName = SystemTabNames.PageHeader, Order = 30)]        
        [AllowedTypes(typeof(ArticleFolderCategory))]
        [UIHint(CategoryUIHint.Category)]
        [DefaultDragAndDropTarget]
        [Required]
        [OptionBarItem]
        public virtual ContentReference ArticleCategory { get; set; }


        [Display(Name = "Type of Articles",
          Order = 50,
          GroupName = SystemTabNames.Content,
          Description = "This will determine the type of articles in this section.")]
        [EnumSelect(typeof(ArticleTypes))]
        [OptionBarItem]
        public virtual ArticleTypes ArticleType { get; set; }

        [CultureSpecific]
        [Searchable]
        [Display(
            Name = "Featured Article(s)",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [AllowedTypes(new[] { typeof(BaseArticlePage), typeof(ArticleTopPostBlock) })]       
        public virtual ContentArea ArticleContentArea { get; set; }

        [CultureSpecific]
        [Searchable]
        [Display(
            Name = "Article Categories",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [AllowedTypes(new[] { typeof(ArticleCategoryLinkBlock) })]
        public virtual ContentArea ArticleCategories { get; set; }
    }
}
