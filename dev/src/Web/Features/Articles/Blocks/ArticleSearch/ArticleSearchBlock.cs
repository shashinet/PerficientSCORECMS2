using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Articles.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Articles.Blocks.ArticleSearch
{
    [ContentType(
        GroupName = GroupNames.Articles,
        DisplayName = "Article Search",
        GUID = "73BE451B-F73E-4802-9F61-F6560E3FFCC0",
        Description = "Display article posts based on configuration")]

    public class ArticleSearchBlock : BaseBlock, IPageContentBlock
    {
        [Display(Name = "Article Root",
         Description = "Parent of articles that you would like to search.",
         GroupName = SystemTabNames.Content,
         Order = 110)]
        public virtual ContentReference ArticleRoot { get; set; }

        [Display(Name = "Article Type",
          Description = "What type of article would you like to display.",
          GroupName = SystemTabNames.Content,
          Order = 110)]
        [EnumSelect(typeof(ArticleTypes))]
        public virtual ArticleTypes TypeOfArticle { get; set; }

        [Display(Name = "Article Layout",
          Description = "Display articles in a grid or list layout.",
          GroupName = SystemTabNames.Content,
          Order = 110)]
        [EnumSelect(typeof(ArticleTypes))]
        public virtual ArticleLayout ArticleLayout { get; set; }
                
        [Display(Name = "Page Size",
            GroupName = SystemTabNames.Content,
            Description = "Number of results per page.",
            Order = 90)]
        public virtual int PageSize { get; set; }
        
        [CultureSpecific]
        [Display(Name = "No Results Found Text",
            GroupName = SystemTabNames.Content,
            Description = "Text to display when no results have been found for the search term.",
            Order = 170)]
        public virtual string NoResultsText { get; set; }

        [CultureSpecific]
        [Display(Name = "Search Text Placeholder",
            GroupName = SystemTabNames.Content,
            Description = "Text to display in the search box before a user searches.",
            Order = 170)]
        public virtual string SearchTextPlaceholder { get; set; }


        [Display(Name = "Show Search Box?",
          Description = "If unchecked then no search box will appear",
          GroupName = SystemTabNames.Content,
          Order = 110)]
        [CultureSpecific]
        [OptionBarItem]
        public virtual bool ShowSearchBox { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            NoResultsText = "No results found";
            ShowSearchBox = true;
        }
    }
}
