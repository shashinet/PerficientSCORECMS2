using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Tile
{
    [ContentType(
       GroupName = GroupNames.Buttons,
       DisplayName = "Tile",
       GUID = "8C25B534-9954-4AA9-927C-AD2DE1ED2D45",
       Description = "Tile button consisting of an Image, and Title")]
    [ImageUrl("~/icons/cms/blocks/CMS-icon-block-13.png")]
    [DisplayOptions(new[] {
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.Fixed })]
    [IndexInContentAreas]
    public class TileBlock : BaseBlock, ICallToActionBlock, INestedContentBlock
    {

        [Display(
           Name = "Image",
            GroupName = SystemTabNames.Content,
            Order = 10
        )]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia) })]
        [DefaultDragAndDropTarget]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }

        [Display(
            Order = 20,
            GroupName = SystemTabNames.Content
        )]
        [CultureSpecific]
        [Searchable]
        public virtual string Title { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Link",
            Order = 30
        )]
        [CultureSpecific]
        [OptionBarItem]
        public virtual Url Link { get; set; }

        [Display(
            Name = "Open in new window?",
            Description = "Check to open link in a new browser window or tab.",
            Order = 40)]
        [OptionBarItem]
        [CultureSpecific]
        public virtual bool OpenInNewWindow { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Order = 60,
            Name = "Tile Style"
        )]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("TileClasses")]
        public virtual string TileStyles { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(this.TileStyles))
            {
                classes += $" {this.TileStyles.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            Title = "[Tile]";
            TileStyles = "default";
        }
    }
}
