using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Interfaces;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation
{
    public class SideNavigationBlockComponent : AsyncBlockComponent<SideNavigationBlock>
    {
        private readonly ISideNavigationBlockService _sideNavigationService;

        public SideNavigationBlockComponent(ISideNavigationBlockService sideNavigationService)
        {
            _sideNavigationService = sideNavigationService;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(SideNavigationBlock currentContent)
        {
            //var viewModel = _sideNavigationService.CreateSideNavigation(currentContent);
            //return await Task.FromResult(View("~/Features/Blocks/Fields/SideNavigation/Views/SideNavigationBlock.cshtml", viewModel));

            var secViewModel = _sideNavigationService.CreateSecondaryNavigation(currentContent);
            return await Task.FromResult(View("~/Features/Blocks/Fields/SideNavigation/Views/SideNavigation.cshtml", secViewModel));
        }
    }
}
