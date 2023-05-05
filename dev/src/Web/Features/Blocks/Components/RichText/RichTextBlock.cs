using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.RichText
{
    /// <summary>
    /// Used to insert a block with text
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = " Rich Text",
        GUID = "3c2ed8a8-6f1a-4145-9d44-98cfeb8a5652",
        Description = "Text block with text editor."
    )]
    [ImageUrl("~/icons/score/epi_score128_text.png")]
    [IndexInContentAreas]
    public class RichTextBlock : BaseBlock, INestedContentBlock, IOnPageEditHelperPanel
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Main Body",
            Order = 100
        )]
        [Searchable]
        [CultureSpecific]
        public virtual XhtmlString MainBody { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            MainBody = new XhtmlString("[Rich Text]");
        }
    }
}
