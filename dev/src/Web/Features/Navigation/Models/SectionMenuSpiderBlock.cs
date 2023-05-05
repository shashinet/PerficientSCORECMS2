using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using Perficient.Infrastructure.DisplayOptions.Constants;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
        GroupName = GroupNames.Navigation,
        DisplayName = "Section Menu Spider",
        GUID = "2915a712-235f-44ee-882c-b0a4e36670ee",
        Description = "Given a root page, crawls through the tree and displays children")]
    [ImageUrl("~/icons/score/epi_score128_subnav_Spider.png")]
    [DisplayOptions(new[] { DisplayOptionConstants.DisplayOptionNames.Full, DisplayOptionConstants.DisplayOptionNames.Half, DisplayOptionConstants.DisplayOptionNames.TwoThirds, DisplayOptionConstants.DisplayOptionNames.OneThird, DisplayOptionConstants.DisplayOptionNames.ThreeFourth, DisplayOptionConstants.DisplayOptionNames.OneFourth })]
    public class SectionMenuSpiderBlock : BaseBlock
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Name = "Parent Page",
            Order = 100)]
        [CultureSpecific]
        [AllowedTypes(typeof(BasePage))]
        [OptionBarItem]
        [FullRefresh]
        public virtual PageReference ParentPage { get; set; }

        #region Injected via Controller
        public List<SectionMenuSpiderData> SpiderData;
        #endregion
    }
}
