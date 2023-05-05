using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Testimonial
{
    [ContentType(
    GroupName = GroupNames.Content,
    DisplayName = "Testimonial",
    GUID = "{23CC7B7A-A02D-457C-98F0-3A54AD0D8DAF}",
    Description = "Testimonial Block component")]
    [ImageUrl("~/icons/score//testimonial-thumb.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Fixed, DisplayOptionConstants.DisplayOptionNames.Offset,
        DisplayOptionConstants.DisplayOptionNames.Contained, DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half, })]
    [IndexInContentAreas]
    public class TestimonialBlock : BaseBlock, INestedContentBlock
    {
        [Display(GroupName = SystemTabNames.Content,
            Order = 20)]
        [UIHint(UIHint.Textarea)]
        [Required]
        [CultureSpecific]
        public virtual string Quote { get; set; }

        [Display(GroupName = SystemTabNames.Content,
            Order = 30)]
        [CultureSpecific]
        public virtual string Source { get; set; }


        [Display(GroupName = SystemTabNames.Content,
            Order = 60)]
        [UIHint(UIHint.Image)]
        [AllowedTypes(new[] { typeof(SvgMedia), typeof(ImageMediaData) })]
        [DefaultDragAndDropTarget]
        [CultureSpecific]
        public virtual ContentReference Image { get; set; }
    }
}
