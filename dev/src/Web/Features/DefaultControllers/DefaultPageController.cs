using EPiServer;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Interfaces.ViewModels;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.Models.ViewModels;
using System;
using System.Linq;

namespace Perficient.Web.Features.DefaultControllers
{
    // This controller provides a default controller for all pages inheriting from type BasePage so we don't have to define a controller for
    // every model in order for it to render. TemplateDescriptor with 'Inherited=true' allows all BasePage items to use this. However, you can specify
    // a controller for more specific types which will take effect over this.
    [TemplateDescriptor(Inherited = true)]
    public class DefaultPageController : PageController<BasePage>
    {
        public ActionResult Index(BasePage currentPage)
        {
            var model = CreateModel(currentPage);
            var viewName = currentPage.GetOriginalType().Name;//.Namespace.Split('.').Last();
            //return View(viewFolderName, model);
            return View(viewName,model);
        }

        public static IContentViewModel<BasePage> CreateModel(BasePage page)
        {
            var type = typeof(ContentViewModel<>).MakeGenericType(page.GetOriginalType());
            return Activator.CreateInstance(type, page) as IContentViewModel<BasePage>;
        }
    }
}
