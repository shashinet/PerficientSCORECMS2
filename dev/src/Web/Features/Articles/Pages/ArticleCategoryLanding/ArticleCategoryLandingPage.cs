using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.Categories;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Web.Features.Articles.Models;
using Perficient.Web.Features.Pages.BaseContentPage;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Pages.ArticleCategoryLanding
{
    [ContentType(
         GroupName = GroupNames.Articles,
         DisplayName = "Article Category Landing Page",
         GUID = "f3920cc8-a62f-4566-9199-6d4791730acd",
         Description = "This page is used to display all Articles of a selected category")]
    [ImageUrl("~/icons/score/epi_score128_page_1col.png")]
    public class ArticleCategoryLandingPage : BaseContentPage
    {

        [Display(Name = "Sub Title",
          GroupName = SystemTabNames.Content,
          Order = 20)]
        [CultureSpecific]
        [Searchable]        
        public virtual string SubTitle { get; set; }

        [Display(Name = "Summary Description",
          GroupName = SystemTabNames.Content,
          Order = 30)]
        [CultureSpecific]
        [Searchable]        
        public virtual string Summary { get; set; }

        [Display(Description = "Blog Category",
            GroupName = SystemTabNames.
            Content, Order = 40)]
        [UIHint(CategoryUIHint.Category)]
        [AllowedTypes(new[] { typeof(ArticleCategory) })]
        [Required]
        [OptionBarItem]        
        public virtual ContentReference ArticleCategory { get; set; }       
    }
}
