using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.Categories;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Articles.Models;
using Perficient.Web.Features.Articles.Pages.ArticleCategoryLanding;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Blocks.ArticleCategoryLink
{

    [ContentType(DisplayName = "Article Category Link Block",
       GUID = "68a6f022-9c79-437b-b3cc-3171978c7b88",
       Description = "This block displays a link to an article category.",
       GroupName = GroupNames.Articles)]
    public class ArticleCategoryLinkBlock : BaseBlock
    {
        [UIHint(CategoryUIHint.Category)]
        [Required]
        [OptionBarItem]
        [AllowedTypes(new[] { typeof(ArticleCategory) })]
        public virtual ContentReference Category { get; set; }

        [Required]
        [OptionBarItem]
        [AllowedTypes(typeof(ArticleCategoryLandingPage))]
        public virtual ContentReference CategoryLandingPage { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            IndexInContentAreas = true;
        }
    }
}
