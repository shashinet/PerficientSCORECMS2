using Newtonsoft.Json;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;

namespace Perficient.Web.Features.Navigation.ViewModels
{
    public class MenuListViewModel : BaseBlockViewModel
    {
        public MenuListViewModel()
        {
            MenuListContent = new List<object>();
            ContentType = "MenuList";
        }

        [JsonProperty("sectionTitle")]
        public string SectionTitle { get; set; }

        [JsonProperty("sectionUrl")]
        public string SectionUrl { get; set; }

        [JsonProperty("menuListContent")]
        public List<object> MenuListContent { get; set; }
    }
}
