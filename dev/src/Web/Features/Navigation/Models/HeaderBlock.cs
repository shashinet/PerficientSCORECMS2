using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Media;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
        GroupName = GroupNames.Navigation,
        DisplayName = "Site Header",
        GUID = "751716CC-0DB9-4B57-98EE-74B69F180864",
        Description = "A block representing the Website Header.")]
    [ImageUrl("~/icons/score/epi_score128_top_menu.png")]
    [DisplayOptions(false)]
    public class HeaderBlock : BaseBlock, IHeaderBlock, IOnPageEditHelperPanel
    {
        [Display(
            Name = "Logo",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [UIHint(UIHint.Image)]
        [AllowedTypes(new[] { typeof(SvgMedia), typeof(ImageMediaData) })]
        [DefaultDragAndDropTarget]
        [CultureSpecific]
        public virtual ContentReference Logo { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Header Logo URL", Order = 20)]
        [CultureSpecific]
        public virtual Url LogoLink { get; set; }

        [Display(
            Name = "Tag Line",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [CultureSpecific]
        public virtual XhtmlString TagLine { get; set; }

        [Display(
            Name = "Navigation Items",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(MegaMenuFlyoutBlock), typeof(NavigationLinkBlock) }, new[] { typeof(SocialIconBlock) })]
        [DisplayOptions(false)]
        public virtual ContentArea NavigationItems { get; set; }

        [Display(
            Name = "Utility Navigation",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        [CultureSpecific]
        [AllowedTypes(typeof(ICallToActionBlock), typeof(MegaMenuFlyoutBlock), typeof(UtilitySearchBlock))]
        [DisplayOptions(false)]
        public virtual ContentArea UtilityNavigation { get; set; }

        #region Styles

        [Display(GroupName = TabNames.Styles, Order = 10, Name = "Header Style")]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("HeaderClasses")] // name of the class selection property on ScoreSettingsPage
        [FullRefresh]
        public virtual string HeaderStyle { get; set; }

        public override string GetClassList()
        {
            var classes = base.GetClassList(HeaderStyle);

            return classes;
        }

        #endregion
    }
}
