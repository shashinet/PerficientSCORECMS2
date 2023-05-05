using EPiServer.Core;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Infrastructure.Models.Content
{
    public class PreviewModel : ContentViewModel<BasePage>
    {
        public PreviewModel(BasePage currentPage, IContent previewContent) : base(currentPage)
        {
            PreviewContent = previewContent;
            Areas = new List<PreviewArea>();
        }

        public IContent PreviewContent { get; set; }
        public List<PreviewArea> Areas { get; set; }

        public class PreviewArea
        {
            public bool Supported { get; set; }
            public string AreaName { get; set; }
            public string AreaTag { get; set; }
            public ContentArea ContentArea { get; set; }
        }
    }
}
