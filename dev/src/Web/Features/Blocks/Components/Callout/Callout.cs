using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Callout
{
    /// <summary>
    /// Used to insert a block with call out text
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Callout",
        GUID = "353958d2-bb56-4198-9453-1ce59672d2bc",
        Description = "Block with callout lead text section.")]
    [ImageUrl("~/icons/score/epi_score128_introduction.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Full, DisplayOptionConstants.DisplayOptionNames.Fixed, DisplayOptionConstants.DisplayOptionNames.Offset,
        DisplayOptionConstants.DisplayOptionNames.Half,DisplayOptionConstants.DisplayOptionNames.OneThird,DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.OneFourth,DisplayOptionConstants.DisplayOptionNames.ThreeFourth,DisplayOptionConstants.DisplayOptionNames.Contained})]
    [IndexInContentAreas]
    public class Callout : BaseBlock, INestedContentBlock
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Main Body",
            Order = 100
        )]
        [CultureSpecific]
        public virtual XhtmlString MainBody { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Order = 30,
            Name = "Callout Style"
        )]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("CalloutClasses")]
        public virtual string CalloutStyle { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(this.CalloutStyle))
            {
                classes += $" {this.CalloutStyle.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            MainBody = new XhtmlString("[Callout]");
            CalloutStyle = "default";
        }
    }
}
