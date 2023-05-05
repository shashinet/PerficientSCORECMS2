using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Buttons;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Button
{
    /// <summary>
    /// Used to insert a button block
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Buttons,
        DisplayName = "Button",
        GUID = "a04fb1c1-2d59-4151-ae9d-efe0b367ba1f",
        Description = "Button component"
    )]
    [ImageUrl("~/icons/score/epi_score128_buttonGeneric.png")]
    public class ButtonBlock : BaseBlock, INestedContentBlock, ICallToActionBlock
    {
        [Display(
        GroupName = SystemTabNames.Content,
        Name = "Button Style",
        Order = 200
        )]
        [CultureSpecific]
        [SelectOne(SelectionFactoryType = typeof(ButtonStyleSelectionFactory))]
        [FullRefresh]
        public virtual string ButtonStyle { get; set; }

        [Display(
            Order = 300,
            GroupName = SystemTabNames.Content,
            Name = "Button Text"
        )]
        [CultureSpecific]
        public virtual string ButtonText { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Button Link",
            Order = 310
        )]
        [CultureSpecific]
        [OptionBarItem]
        public virtual Url ButtonLink { get; set; }

        [Display(
            Name = "Open in new window?",
            Description = "Check to open link in a new browser window or tab.",
            Order = 320)]
        [OptionBarItem]
        [CultureSpecific]
        public virtual bool OpenInNewWindow { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(this.ButtonStyle))
            {
                classes += $" {this.ButtonStyle.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            ButtonText = "[Button]";
            ButtonStyle = "primary";
        }
    }
}
