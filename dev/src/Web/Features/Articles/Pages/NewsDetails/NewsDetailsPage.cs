using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Web.Features.Articles.Models;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Pages.NewsDetails
{
    [ContentType(
         GroupName = GroupNames.Articles,
         DisplayName = "News Details Page",
         GUID = "DEB70280-4692-4BEA-8667-251E7DD450C2",
         Description = "News Details Page. This page is used to create news article")]
    [ImageUrl("~/icons/score/epi_score128_page_1col.png")]
    public class NewsDetailsPage : BaseArticlePage, INestedContentBlock
    {
        [Display(Name = "Author",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        [CultureSpecific]
        [Searchable]
        [OptionBarItem]
        [FullRefresh]        
        public virtual string Author { get; set; }
    }
}
