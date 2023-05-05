using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find.Cms;
using EPiServer.Personalization.Content.UI.FrontEnd.Blocks;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.EditorDescriptors.Alignment;
using Perficient.Infrastructure.EditorDescriptors.Colors;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Blocks.Fields.Heading;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Layouts.Stripe
{
    [ContentType(
         GroupName = GroupNames.Layout,
         DisplayName = "Stripe",
         GUID = "0203eb27-32cc-4ab7-b5d0-88ee731522b0",
         Description = "A stripe is a background element that spans edge to edge. Default stripes can have a background color or single image.")]
    [ImageUrl("~/icons/score/epi_score128_stripe_background.png")]
    [DisplayOptions(false)]
    [IndexInContentAreas]
    public class StripeBlock : BaseBlock, IPageContentBlock, IOnPageEditHelperPanel
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Background Image",
            Order = 110)]
        [CultureSpecific]
        [UIHint(UIHint.Image)]
        [OptionBarItem]
        [FullRefresh]
        public virtual ContentReference BackgroundImage { get; set; }

        [Display(Name = "Title",
           Order = 100)]
        [HeadingStyles("H2", "H3", "H4", "H5")]
        public virtual HeadingBlock Title { get; set; }


        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Main Content Area",
            Order = 130)]
        [CultureSpecific]
        //TODO: Add in Forms and Content Reccomendations
        [AllowedTypes(typeof(IPageContentBlock), typeof(INestedContentBlock), typeof(ContentRecommendationsBlock))]
        [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.OneFourth,
            DisplayOptionConstants.DisplayOptionNames.OneThird,
            DisplayOptionConstants.DisplayOptionNames.Half,
            DisplayOptionConstants.DisplayOptionNames.TwoThirds,
            DisplayOptionConstants.DisplayOptionNames.ThreeFourth,
            DisplayOptionConstants.DisplayOptionNames.Full
        })]
        public virtual ContentArea MainContentArea { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Call To Action Buttons",
            Order = 140)]
        [CultureSpecific]
        [AllowedTypes(typeof(ICallToActionBlock))]
        [DisplayOptions(false)]
        public virtual ContentArea CtaContentArea { get; set; }

        [Display(GroupName = TabNames.Styles, Order = 10, Name = "Text Color")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(FontColorPickerEditorDescriptor))]
        [UIHint("FontColorPickerEditor")]
        [OptionBarItem]
        [FullRefresh]
        public virtual string TextColor { get; set; }

        [Display(GroupName = TabNames.Styles, Order = 20, Name = "Background Color")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(ColorPickerEditorDescriptor))]
        [UIHint("ColorPickerEditor")]
        [OptionBarItem]
        [FullRefresh]
        public virtual string BackgroundColor { get; set; }

        [Display(GroupName = TabNames.Styles, Order = 30, Name = "Stripe Style")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("StripeClasses")] // name of the class selection property on ScoreSettingsPage
        [OptionBarItem]
        [FullRefresh]
        public virtual string StripeStyle { get; set; }

        [Display(GroupName = TabNames.Styles, Name = "Header Content Alignment", Order = 50)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(TextAlignmentEditorDescriptor))]
        [UIHint("TextAlignmentEditor")]
        public virtual string HeaderContentAlignment { get; set; }

        [Display(GroupName = TabNames.Styles, Name = "Main Content Area Alignment", Order = 50)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(AlignmentEditorDescriptor))]
        [UIHint("AlignmentEditor")]
        public virtual string MainContentAreaAlignment { get; set; }

        [Display(GroupName = TabNames.Styles, Name = "CTA Content Alignment", Order = 50)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(AlignmentEditorDescriptor))]
        [UIHint("AlignmentEditor")]
        public virtual string CtaContentAlignment { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            Title.Text = "[Stripe]";
            Title.HeadingStyle = "H2";
            StripeStyle = "default";
        }

        public override string GetClassList()
        {
            var classes = base.GetClassList();

            if (!string.IsNullOrWhiteSpace(StripeStyle))
            {
                classes += $" {StripeStyle.Replace(",", " ")}";
            }

            return classes;
        }
    }
}
