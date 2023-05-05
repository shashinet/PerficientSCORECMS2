using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
      GroupName = GroupNames.Navigation,
      DisplayName = "Social Icons",
      GUID = "{EB6150E7-E6E5-4AD1-B5CA-2FDBD6B7BF15}",
      Description = "Social Media Icon with link")]
    [ImageUrl("~/icons/cms/blocks/twitter.png")]
    [DisplayOptions(false)]
    public class SocialIconBlock : NavigationLinkBlock, IFooterBlock
    {
        [Display(GroupName = SystemTabNames.Content,
            Name = "Social Logo",
            Order = 20)]
        [CultureSpecific]
        [UIHint(UIHint.Image)]
        [AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia) })]
        [DefaultDragAndDropTarget]
        [FullRefresh]
        [OptionBarItem]
        public virtual ContentReference Image { get; set; }
    }
}
