using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Navigation.Models;
using Perficient.Web.Features.Navigation.Services;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Navigation.Controllers
{
    public class FooterBlockComponent : AsyncPartialContentComponent<FooterBlock>
    {
        private readonly INavigationService _navigationService;
        public FooterBlockComponent(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(FooterBlock currentBlock)
        {
            var footer = _navigationService.GetFooter(currentBlock);

            return await Task.FromResult(View("~/Features/Navigation/Views/_FooterBlock.cshtml", footer));
        }
    }
}
