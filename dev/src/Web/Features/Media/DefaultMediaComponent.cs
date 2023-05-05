using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Media
{
    [TemplateDescriptor(Inherited = true)]
    public class DefaultMediaComponent : AsyncPartialContentComponent<MediaData>
    {
        public DefaultMediaComponent() { }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(MediaData currentContent)
        {
            switch (currentContent)
            {
                case ImageMediaData image:
                    return await Task.FromResult(View("~/Features/Media/ImageMediaData.cshtml", image));
                default:
                    // BUG: Index.cshtml is not found
                    return await Task.FromResult(View("~/Features/Media/Index.cshtml", currentContent.GetType().BaseType.Name));
            }
        }
    }
}
