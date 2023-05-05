using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors.SelectionFactories;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles
{
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Search Page",
        GUID = "c1b1dc3e-6d60-4b3a-b06d-86f92c43b2de",
        Description = "Search the site pages")]
    [ImageUrl("~/icons/score/epi_score128_page_1col.png")]
    public class ArticleSearchPage : BasePage
    {
        [CultureSpecific]
        [Searchable]
        [Required]
        [Display(Name = "Title",
           Description = "Page Title",
           GroupName = SystemTabNames.Content,
           Order = 10)]
        public virtual string Title { get; set; }

        [Display(
          GroupName = SystemTabNames.Content,
          Name = "Main Content",
          Order = 20)]
        [CultureSpecific]
        public virtual XhtmlString MainContent { get; set; }

        [Display(Name = "Show Search Box", GroupName = TabNames.Search, Order = 30)]
        public virtual bool ShowSearchBox { get; set; }

        [CultureSpecific]
        [Display(Name = "Search Textbox Place Holder Text",
          GroupName = TabNames.Search,
          Order = 40)]
        public virtual string SearchPlaceHolderText { get; set; }

        [Display(Name = "Page Size", Order = 50, GroupName = TabNames.Search)]
        public virtual int PageSize { get; set; }

        [Display(
         GroupName = TabNames.Search,
         Name = "No Results Message",
         Order = 60)]
        [CultureSpecific]
        public virtual XhtmlString NoResultsMessage { get; set; }

        [Display(GroupName = TabNames.Search,
            Name = "Types of Pages to Find",
            Order = 70)]
        [SelectMany(SelectionFactoryType = typeof(PageTypeSelectionFactory))]
        [UIHint("PageTypeSelectionFactory")]
        public virtual string PageTypes { get; set; }

        [Display (Name = "Only These Roots", Description = "Only find pages at a given root and below", GroupName = TabNames.Search, Order = 80)]
        [DefaultDragAndDropTarget]
        [OptionBarItem]
        [AllowedTypes(typeof(BasePage))]
        public virtual IList<ContentReference> IncludedRoots { get; set; }
    }
}
