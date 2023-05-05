using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
      GroupName = GroupNames.Navigation,
      DisplayName = "Search Box",
      GUID = "{FC52B5CC-099E-4ABA-BDA3-8771F2192988}",
      Description = "Utility Navigation Search Box")]
    [ImageUrl("~/icons/score/epi_score128_searchBox.png")]
    [DisplayOptions(false)]
    public class UtilitySearchBlock : BaseBlock
    {
        [Display(Name = "Title", GroupName = SystemTabNames.Content, Order = 45)]
        [CultureSpecific]
        public virtual string Title { get; set; }

        [Display(Name = "Search Placeholder Text", GroupName = SystemTabNames.Content, Order = 50)]
        [CultureSpecific]
        public virtual string SearchPlaceholderText { get; set; }

        [Display(Name = "Target Search Result Page", GroupName = SystemTabNames.Content, Order = 55)]
        [AllowedTypes(typeof(BasePage))]
        [DefaultDragAndDropTarget]
        public virtual ContentReference SearchPage { get; set; }
    }
}
