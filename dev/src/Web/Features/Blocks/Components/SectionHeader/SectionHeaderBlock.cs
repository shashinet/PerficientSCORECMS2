using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using EPiServer.Web;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.SectionHeader
{
    /// <summary>
    /// Used to insert a section header component
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Section Header",
        GUID = "a48d7f69-8480-4ecd-b0f0-f743ace5e6ac",
        Description = "Section header component"
    )]
    [ImageUrl("~/icons/score/epi_score128_section_header.png")]
    [DisplayOptions(new[] {
        DisplayOptionConstants.DisplayOptionNames.EdgeToEdge,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.Offset,
        DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half
    })]
    [IndexInContentAreas]
    public class SectionHeaderBlock : BaseBlock, IPageContentBlock, INestedContentBlock
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 100
        )]
        [CultureSpecific]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }

        [Display(
            Order = 200,
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        [Searchable]
        public virtual string Title { get; set; }

        [Display(
            Order = 300,
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        [Searchable]
        public virtual string Subtitle { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            Title = "[Section Header]";
        }
    }
}
