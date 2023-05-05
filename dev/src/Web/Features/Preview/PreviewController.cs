using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Framework.Web.Mvc;
using EPiServer.Web;

using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.Models.ViewModels;

namespace Perficient.Web.Features.Preview
{
    [TemplateDescriptor(
       Inherited = true,
       TemplateTypeCategory = TemplateTypeCategories.MvcController, //Required as controllers for blocks are registered as MvcPartialController by default
       Tags = new[] { RenderingTags.Preview, RenderingTags.Edit },
       AvailableWithoutTag = false)]
    [RequireClientResources]
    public class PreviewController : Controller, IRenderTemplate<BlockData>
    {
        private readonly IContentLoader _contentLoader;

        public PreviewController(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public ActionResult Index(IContent currentContent)
        {
            //As the layout requires a page for title etc we "borrow" the start page
            var startPage = _contentLoader.Get<PageData>(ContentReference.StartPage) as BaseStartPage;

            var model = new PreviewViewModel(startPage, currentContent);

            return View("~/Features/Preview/_GeneralPreview.cshtml", model);
        }
    }
}
