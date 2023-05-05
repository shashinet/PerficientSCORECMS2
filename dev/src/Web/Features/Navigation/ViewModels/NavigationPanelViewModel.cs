using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class NavigationPanelViewModel : BaseBlockViewModel
    {
        public NavigationPanelViewModel()
        {
            ColumnContent = new List<object>();
            ContentType = "NavigationPanel";
        }

        [JsonProperty("columnContent")]
        public List<object> ColumnContent { get; set; }
    }
}
