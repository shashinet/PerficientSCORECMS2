using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Fields.YouTube
{
    [ContentType(
        DisplayName = "YouTube Base",
        GUID = "3e1df5a8-bd91-4009-b625-499d9bacc24f",
        AvailableInEditMode = false)]
    public class YouTubeBaseBlock : BaseBlock, INestedContentBlock
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 310)]
        public virtual string VideoId { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 315)]
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
