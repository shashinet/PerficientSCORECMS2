using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Interfaces.BlockTypes;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Navigation.Models
{
    [ContentType(
        GroupName = GroupNames.Navigation,
        DisplayName = "Breadcrumbs",
        GUID = "946EA19A-5E34-49CE-AD87-EA365A3164BF",
        Description = "Renders a breadcrumb navigation, crawling up to the site's Home Page")]
    [ImageUrl("~/icons/score/epi_score128_breadcrumb.png")]
    public class BreadcrumbBlock : BaseBlock
    {
        [ScaffoldColumn(false)]
        [Ignore]
        [UIHint(UIHint.Image)]
        public virtual ContentReference HomeIcon { get; set; }

        [ScaffoldColumn(false)]
        [Ignore]
        public virtual List<BaseLinkViewModel> Links { get; set; }
    }
}
