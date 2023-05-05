using Perficient.Web.Features.Blocks.Fields.SideNavigation.Models;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.Interfaces
{
    public interface ISideNavigationBlockService
    {
        SideNavigationViewModel CreateSideNavigation(SideNavigationBlock currentBlock);

        SideNavigationReactViewModel CreateSecondaryNavigation(SideNavigationBlock currentBlock);
    }
}
