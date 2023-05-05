using System;

namespace Perficient.Web.Features.Articles.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string ArticleUrl { get; set; }
        public string Title { get; set; }        
        public string Summary { get; set; }
        public string Category { get; set; }
        public string CategoryColor { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
