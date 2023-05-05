using System.Collections.Generic;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.Models
{
    public class SideNavigationLinkViewModel
    {
        public string Title { get; set; }

        public string AriaLabel { get; set; }

        public string Link { get; set; }

        public bool OpenInNewWindow { get; set; }

        public List<SideNavigationLinkViewModel> ChildLinks { get; set; }

        public bool HasChildLinks => ChildLinks != null && ChildLinks.Count > 0;

        public bool IsActive { get; set; }
    }
}
