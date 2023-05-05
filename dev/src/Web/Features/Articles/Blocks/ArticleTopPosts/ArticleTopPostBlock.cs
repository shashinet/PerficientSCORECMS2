using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Articles.Models;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Blocks.ArticlesTopPosts
{
    /// <summary>
    /// Used to insert a blog top and featured post block component    
    /// </summary>
    [ContentType(
            GroupName = GroupNames.Articles,
            DisplayName = "Blog Top Post",
            GUID = "f925c0f4-9dfb-4ffe-a53f-7d39e41a6227",
            Description = "Blog Top & Featured Posts")]             
        
        public class ArticleTopPostBlock : BaseBlock, IPageContentBlock
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
               Description = "Featured Posts",
               GroupName = SystemTabNames.Content,
               Name = "Featured Posts",
               Order = 20)]
        [CultureSpecific]
        [MaxElements(2)]
        [AllowedTypes(typeof(BaseArticlePage))]
        public virtual ContentArea FeaturedPosts { get; set; }

        [Display(
            Description = "Top Posts",
            GroupName = SystemTabNames.Content,
            Name = "Top Posts",
            Order = 30)]
        [CultureSpecific]
        [AllowedTypes(typeof(BaseArticlePage))]
        [MaxElements(4)]
        public virtual ContentArea TopPosts { get; set; }

            

    }    
}
