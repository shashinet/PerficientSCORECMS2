using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
        GroupName = GroupNames.Navigation,
        DisplayName = "Mega Menu Flyout",
        GUID = "5f0e0568-6d77-4d74-9b71-371d8a76a58c",
        Description = "A mega menu flyout section to be used within the header.")]
    [ImageUrl("~/icons/score/epi_score128_megaMenuDroplist.png")]
    [DisplayOptions(false)]
    public class MegaMenuFlyoutBlock : BaseBlock
    {
        [Display(Name = "Title", GroupName = SystemTabNames.Content, Order = 20)]
        [CultureSpecific]
        public virtual string MenuTitle { get; set; }

        [Display(Name = "Navigation Panels", GroupName = SystemTabNames.Content, Order = 40)]
        [AllowedTypes(typeof(NavigationPanelBlock))]
        [DisplayOptions(false)]
        [DefaultDragAndDropTarget]
        [CultureSpecific]
        public virtual ContentArea NavigationPanels { get; set; }

        [CultureSpecific]
        [Display(Name = "CTA Buttons", Order = 50)]
        [AllowedTypes(typeof(ICallToActionBlock))]
        public virtual ContentArea CallToActionContentArea { get; set; }
    }
}
