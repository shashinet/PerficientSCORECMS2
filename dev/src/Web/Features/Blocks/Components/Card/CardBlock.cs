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
using Perficient.Web.Features.Blocks.Fields.Heading;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Components.Card
{
    /// <summary>
    /// Used to insert a highlight block component
    /// </summary>
    [ContentType(
        GroupName = GroupNames.Content,
        DisplayName = "Card",
        GUID = "d15097e8-c001-434b-8e2b-01e0e2245f3f",
        Description = "Card component"
    )]
    [ImageUrl("~/icons/score/epi_score128_highlight.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Contained,
        DisplayOptionConstants.DisplayOptionNames.Fixed,
        DisplayOptionConstants.DisplayOptionNames.OneFourth,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.Full })]

    [IndexInContentAreas]
    public class CardBlock : BaseBlock, INestedContentBlock
    {
        [Display(
            Order = 10,
            GroupName = SystemTabNames.Content,
            Name = "Caption Title"
        )]
        [HeadingStyles("H2", "H3", "H4", "H5")]
        public virtual HeadingBlock CaptionHeading { get; set; }

        [Display(
            Order = 20,
            GroupName = SystemTabNames.Content,
            Name = "Caption Subtitle"
        )]
        [HeadingStyles("H2", "H3", "H4", "H5", "P")]
        public virtual HeadingBlock SubHeading { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 30)]
        [UIHint(UIHint.Image)]
        [AllowedTypes(new[] { typeof(ImageMediaData), typeof(SvgMedia) })]
        [DefaultDragAndDropTarget]
        [CultureSpecific]
        public virtual ContentReference Image { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 50,
            Name = "Caption Body"
        )]
        [CultureSpecific]
        public virtual XhtmlString CaptionBody { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 40,
            Name = "Body Description"
        )]
        [CultureSpecific]
        public virtual string BodyDescription { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 60,
            Name = "Call To Action Content Area"
        )]
        [CultureSpecific]
        [AllowedTypes(typeof(ICallToActionBlock))]
        public virtual ContentArea CallToActionContentArea { get; set; }

        [Display(
            GroupName = TabNames.Styles,
            Order = 30,
            Name = "Card Style"
        )]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("CardClasses")] // name of the class selection property on ScoreSettingsPage
        public virtual string CardStyle { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(CardStyle))
            {
                classes += $" {CardStyle.Replace(",", " ")}";
            }

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            CaptionHeading.Text = "[Card]";
            CaptionHeading.HeadingStyle = "H3";
            CardStyle = "default";
        }
    }
}
