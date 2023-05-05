using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Fields;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.Vimeo;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Vimeo
{
    /// <summary>
    /// Used to insert a block with text
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Videos,
        DisplayName = "Vimeo Video",
        GUID = "{F15D131B-79A7-4C83-A5A7-DD52CA6922F7}",
        Description = "Vimeo video component")]
    [ImageUrl("~/icons/score/epi_score128_vimeo.png")]
    public class VimeoBlock : BaseBlock, INestedContentBlock
    {
        [Display(
            Order = 20,
            GroupName = SystemTabNames.Content
        )]
        [EditorDescriptor(EditorDescriptorType = typeof(VimeoEditorDescriptor))]
        public virtual VimeoBaseBlock ScoreVimeo { get; set; }

        [Display(
            Name = "Accessibility Title",
            GroupName = SystemTabNames.Content,
            Order = 22)]
        [CultureSpecific]
        [FullRefresh]
        public virtual string AccessibleTitle { get; set; }
    }
}
