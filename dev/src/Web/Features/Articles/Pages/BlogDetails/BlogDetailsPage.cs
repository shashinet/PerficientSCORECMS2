using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Web.Features.Articles.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Pages.BlogDetails
{
    [ContentType(
         GroupName = GroupNames.Articles,
         DisplayName = "Blog Details Page",
         GUID = "F14224AD-4A63-46C6-8EF5-719EBBA25CA0",
         Description = "Blog Details Page. This page is used to create single blog article")]
    [ImageUrl("~/icons/score/epi_score128_page_1col.png")]
    public class BlogDetailsPage : BaseArticlePage, INestedContentBlock, IContentSaving
    {
        [Display(Name = "Author",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        [CultureSpecific]
        [Searchable]
        [OptionBarItem]
        [FullRefresh]
        public virtual string Author { get; set; }


        [ScaffoldColumn(false)]
        public virtual int ReadTime { get; set; }

        public void SavingContent(object sender, ContentEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            else
            {
                var blogPage = e.Content as BlogDetailsPage;

                if (blogPage == null) { return; }

                decimal wordCount = blogPage.MainContent is null ? 0 : blogPage.MainContent.ToString().Split(" ").Length;
                blogPage.ReadTime = Convert.ToInt16(Math.Round(wordCount / 200));
            }
        }
    }
}
