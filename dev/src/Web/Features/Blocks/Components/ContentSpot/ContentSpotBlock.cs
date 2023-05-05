using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.ContentSpot
{
    /// <summary>
    /// Used to insert a content spot text
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Content Spot",
        GUID = "91ecdc32-b41a-4692-9974-da2b63d2842a",
        Description = "Content spot component")]
    [ImageUrl("~/icons/score/epi_score128_content.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.EdgeToEdge,
        DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.OneFourth })]
    [IndexInContentAreas]
    public class ContentSpotBlock : BaseBlock, INestedContentBlock, IFooterBlock
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Main Body",
            Order = 100
        )]
        [CultureSpecific]
        [Searchable]
        public virtual XhtmlString MainBody { get; set; }

        [Display(GroupName = TabNames.Styles, Order = 110, Name = "Background Color")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(ColorPickerEditorDescriptor))]
        [UIHint("ColorPickerEditor")]
        [OptionBarItem]
        [FullRefresh]
        public virtual string BackgroundColor { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            MainBody = new XhtmlString("[Content Spot]");
        }
    }
}
