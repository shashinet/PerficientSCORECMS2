using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.Models
{
    public class SideNavigationViewModel
    {
        public string CssClasses { get; set; }

        public List<SideNavigationLinkViewModel> NavigationItems { get; set; }

        public bool HasNavigationItems => NavigationItems != null && NavigationItems.Count > 0;
    }
}
