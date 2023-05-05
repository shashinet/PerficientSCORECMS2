using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Geta.Optimizely.Categories;
using Perficient.Infrastructure.Attributes;
using Perficient.Web.Features.Pages.BaseContentPage;
using System;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Models
{
    public abstract class BaseArticlePage : BaseContentPage
    {
        [Display(Name = "Article Category",
            Description = "The main category for the current article",
            GroupName = SystemTabNames.PageHeader,
            Order = 30)]        
        [AllowedTypes(typeof(ArticleCategory))]
        [UIHint(CategoryUIHint.Category)]
        [DefaultDragAndDropTarget]
        [Required]
        [OptionBarItem]
        public virtual ContentReference ArticleCategory { get; set; }


        [Display(Name = "Summary",
            Description = "Article Summary",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [CultureSpecific]
        [Searchable]
        [Required]
        [UIHint(UIHint.Textarea)]
        [OptionBarItem]
        public virtual string Summary { get; set; }

        [Display(
           GroupName = SystemTabNames.Content,
           Name = "Main Content",
           Order = 70)]
        [CultureSpecific]
        public virtual XhtmlString MainContent { get; set; }

        [Display(Name = "Publish Date",
           GroupName = SystemTabNames.Content,
           Order = 20)]
        [OptionBarItem]
        [FullRefresh]
        public virtual DateTime PublishedDate { get; set; }
    }
}
