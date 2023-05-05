using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;


namespace Perficient.Web.Features.Blocks.Components.ImageButton
{
    [ContentType(
       GroupName = GroupNames.Buttons,
       DisplayName = "Image Button",
       GUID = "24702262-6397-43D9-BDA5-FC79E4AB4918",
       Description = "Image Button component"
   )]
    [ImageUrl("~/icons/score/epi_score128_buttonGeneric.png")]
    public class ImageButtonBlock : BaseBlock, ICallToActionBlock
    {

        [Display(
            Order = 100,
            GroupName = SystemTabNames.Content,
            Name = "Button Image"
        )]
        [CultureSpecific]
        //[UIHint(UIHint.Image)]
        [AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia) })]
        [DefaultDragAndDropTarget]
        public virtual ContentReference ButtonImage { get; set; }

        [Display(
            Order = 200,
            GroupName = SystemTabNames.Content,
            Name = "Alt Text for Image"
        )]
        [CultureSpecific]
        public virtual string AltText { get; set; }

        [Display(
          Order = 300,
          GroupName = SystemTabNames.Content,
          Name = "Button Link"
      )]
        [CultureSpecific]
        public virtual Url CalltoActionUrl { get; set; }

        [Display(
           Name = "Open in new window?",
           Description = "Check to open link in a new browser window or tab.",
           Order = 400)]
        [OptionBarItem]
        [CultureSpecific]
        public virtual bool OpenInNewWindow { get; set; }

    }
}
