using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.SelectionFactories;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;
using Perficient.Infrastructure.EditorDescriptors.Alignment;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
        GroupName = GroupNames.Navigation,
        DisplayName = "Site Footer",
        GUID = "{52E318AD-DE29-496F-87C5-3250AA21D387}",
        Description = "Site footer block")]
    [ImageUrl("~/icons/score/footer-block.png")]
    [DisplayOptions(false)]
    public class FooterBlock : BaseBlock, IFooterBlock, IOnPageEditHelperPanel
    {
        #region ContentTab

        [Display(Name = "Upper Footer Navigation",
         GroupName = SystemTabNames.Content,
         Order = 10,
         Description = "These are navigation columns at the top of the footer block. Maximum 5 colums allowed.")]
        [AllowedTypes(typeof(NavigationPanelBlock))]
        [MaxElements(5)]
        [CultureSpecific]
        [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Full,
        DisplayOptionConstants.DisplayOptionNames.Half,
        DisplayOptionConstants.DisplayOptionNames.TwoThirds,
        DisplayOptionConstants.DisplayOptionNames.OneThird,
        DisplayOptionConstants.DisplayOptionNames.OneFourth,
        DisplayOptionConstants.DisplayOptionNames.ThreeFourth })]
        public virtual ContentArea UpperFooterNavBlock { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Footer Logo", Order = 10)]
        [UIHint(UIHint.Image)]
        [CultureSpecific]
        [AllowedTypes(typeof(ImageMediaData), typeof(SvgMedia))]
        public virtual ContentReference Image { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Footer Logo URL", Order = 20)]
        [CultureSpecific]
        public virtual Url ImageLink { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Under Logo Content", Order = 30)]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(SocialIconBlock) })]
        public virtual ContentArea UnderLogoContent { get; set; }

        [Display(GroupName = SystemTabNames.Content,
            Name = "Main Content Area",
            Order = 40,
            Description = "These are navigation columns for the footer block. Maximum 4 columns allowed.")]
        [CultureSpecific]
        [AllowedTypes(typeof(NavigationPanelBlock))]
        [MaxElements(4)]
        [DisplayOptions(new[] {
            DisplayOptionConstants.DisplayOptionNames.OneFourth,
            DisplayOptionConstants.DisplayOptionNames.OneThird,
            DisplayOptionConstants.DisplayOptionNames.Half,
            DisplayOptionConstants.DisplayOptionNames.TwoThirds,
            DisplayOptionConstants.DisplayOptionNames.ThreeFourth,
            DisplayOptionConstants.DisplayOptionNames.Full })]
        public virtual ContentArea MainContentArea { get; set; }
        #endregion

        #region StylesTab

        [Display(GroupName = TabNames.Styles, Order = 30, Name = "Footer Style")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("FooterClasses")] // name of the class selection property on ScoreSettingsPage
        [FullRefresh]
        public virtual string FooterStyle { get; set; }

        [ScaffoldColumn(false)]
        public override string GlobalStyle { get; set; }

        [Display(GroupName = TabNames.Styles, Name = "Footer Content Alignment", Order = 50)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(AlignmentEditorDescriptor))]
        [UIHint("AlignmentEditor")]
        public virtual string FooterContentAlignment { get; set; }
        #endregion

        public override string GetClassList()
        {
            var classes = base.GetClassList(FooterStyle);

            return classes;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            FooterContentAlignment = "justify-start";
            base.SetDefaultValues(contentType);
        }
    }

}
