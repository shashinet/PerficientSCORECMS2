using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
      GroupName = GroupNames.Navigation,
      DisplayName = "Navigation Link Block",
      GUID = "{B8B0A090-F971-47CD-9F3A-DBF9E91DAB86}",
      Description = "Navigation Link to a page or external site")]
    [ImageUrl("~/icons/cms/blocks/CMS-icon-block-12.png")]
    [DisplayOptions(false)]
    public class NavigationLinkBlock : BaseBlock, IFooterBlock, IHeaderBlock
    {
        [Display(GroupName = SystemTabNames.Content,
            Name = "Link Title",
            Order = 10)]
        [CultureSpecific]
        public virtual string Title { get; set; }

        [Display(GroupName = SystemTabNames.Content,
            Name = "Link Url",
            Order = 30)]
        [CultureSpecific]
        [OptionBarItem]
        public virtual Url Link { get; set; }

        [Display(
            Name = "Open in new window?",
            Description = "Check to open link in a new browser window or tab.",
            Order = 40)]
        [OptionBarItem]
        [CultureSpecific]
        public virtual bool OpenInNewWindow { get; set; }
    }
}
