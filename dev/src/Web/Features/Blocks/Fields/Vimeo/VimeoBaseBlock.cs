using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Fields.Vimeo
{
    [ContentType(
        DisplayName = "Vimeo Base",
        GUID = "{C753E6F5-9444-4DFD-BAD3-D1C392ED4048}",
        AvailableInEditMode = false)]
    public class VimeoBaseBlock : BaseBlock, INestedContentBlock
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 310)]
        public virtual string VideoId { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 315)]
        public virtual string VideoThumbnailId { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 317)]
        public virtual string VideoName { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        [DefaultValue(315), Range(0, 1080)]
        public virtual int Height { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 330)]
        [DefaultValue(560), Range(0, 1920)]
        public virtual int Width { get; set; }
    }
}
