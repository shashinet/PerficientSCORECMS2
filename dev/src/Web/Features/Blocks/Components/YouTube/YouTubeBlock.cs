using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Fields;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.YouTube;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.YouTube
{
    /// <summary>
    /// Used to insert a block with text
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Videos,
        DisplayName = "YouTube Video",
        GUID = "7997f1e1-ab36-4a74-b552-25a73b49257a",
        Description = "YouTube video component")]
    [ImageUrl("/icons/cms/blocks/CMS-icon-block-05.png")]
    public class YouTubeBlock : BaseBlock, INestedContentBlock
    {
        [Display(
            Name = "YouTube Video",
            Order = 20,
            GroupName = SystemTabNames.Content
        )]
        [EditorDescriptor(EditorDescriptorType = typeof(YouTubeEditorDescriptor))]
        public virtual YouTubeBaseBlock ScoreYouTube { get; set; }

        [Display(
            Name = "YouTube Video ID Override",
            Description = "This is used for videos that are unlisted/hidden, but still publically accessable. It will override any video selected in the Video Selector.",
            Order = 21,
            GroupName = SystemTabNames.Content
        )]
        [OptionBarItem]
        [FullRefresh]
        public virtual string YouTubeIdOverride { get; set; }

        [Display(
            Name = "Accessibility Title",
            GroupName = SystemTabNames.Content,
            Order = 22)]
        [CultureSpecific]
        [FullRefresh]
        public virtual string AccessibleTitle { get; set; }
    }
}
