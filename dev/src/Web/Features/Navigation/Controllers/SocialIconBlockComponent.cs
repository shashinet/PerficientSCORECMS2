using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Navigation.Models;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Navigation.Controllers
{
    public class SocialIconBlockComponent : AsyncPartialContentComponent<SocialIconBlock>
    {
        protected override async Task<IViewComponentResult> InvokeComponentAsync(SocialIconBlock currentBlock)
        {
            return await Task.FromResult(View("~/Features/Navigation/Views/SocialIconBlock.cshtml", currentBlock));
        }
    }
}
