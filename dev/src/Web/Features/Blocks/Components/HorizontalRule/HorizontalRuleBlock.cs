using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.HorizontalRule
{
    /// <summary>
    /// Used to insert a block with text
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Horizontal Rule",
        GUID = "5CC6F01A-F559-4340-A11F-D66EA8039F11",
        Description = "A horizontal divider."
    )]
    [ImageUrl("~/icons/score/epi_score128_horizontal_rule.png")]
    public class HorizontalRuleBlock : BaseBlock, IPageContentBlock, INestedContentBlock, IOnPageEditHelperPanel
    {
        [Display(
            GroupName = TabNames.Styles,
            Order = 30,
            Name = "Horizontal Rule Style"
        )]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("HorizontalRuleClasses")] // name of the class selection property on ScoreSettingsPage
        public virtual string HorizontalRuleStyle { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            HorizontalRuleStyle = "default";
        }
    }
}
