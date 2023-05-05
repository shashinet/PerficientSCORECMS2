using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Navigation.Models;
using Perficient.Web.Features.Navigation.Services;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Navigation.Controllers
{
    public class HeaderBlockComponent : AsyncPartialContentComponent<HeaderBlock>
    {

        private readonly INavigationService _navigationService;

        public HeaderBlockComponent(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(HeaderBlock currentBlock)
        {
            var header = _navigationService.GetHeader(currentBlock);

            return await Task.FromResult(View("~/Features/Navigation/Views/_HeaderBlock.cshtml", header));
        }
    }
}
