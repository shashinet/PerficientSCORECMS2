using System;

using EPiServer.ContentApi.Core.Configuration;
using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.Core;
using EPiServer.ServiceLocation;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

using Perficient.Infrastructure.Extensions;

namespace Perficient.Infrastructure.RenderingServices
{
    // hijack the base implementation and force it to use canonical URLs for all src properties
    [ServiceConfiguration(typeof(CustomXhtmlRenderService))]
    public class CustomXhtmlRenderService : XhtmlRenderService
    {
        private ILogger logger;

        public CustomXhtmlRenderService()
            : base(ServiceLocator.Current.GetInstance<ContentApiOptions>(),
                   ServiceLocator.Current.GetInstance<IHtmlHelper>(),
                   ServiceLocator.Current.GetInstance<ITempDataProvider>(),
                   ServiceLocator.Current.GetInstance<ICompositeViewEngine>(),
                   ServiceLocator.Current.GetInstance<IModelMetadataProvider>())
        {
            logger = ServiceLocator.Current.GetInstance<ILogger<CustomXhtmlRenderService>>();
        }

        public CustomXhtmlRenderService(ContentApiOptions options,
                                        IHtmlHelper htmlHelper,
                                        ITempDataProvider tempDataProvider,
                                        ICompositeViewEngine compositeViewEngine,
                                        IModelMetadataProvider metadataProvider,
                                        ILogger<CustomXhtmlRenderService> logger)
            : base(options,
                   htmlHelper,
                   tempDataProvider,
                   compositeViewEngine,
                   metadataProvider)
        {
            this.logger = logger;
        }

        public override string RenderXhtmlString(HttpContext context, XhtmlString xhtmlString)
        {
            var result = base.RenderXhtmlString(context, xhtmlString);

            try
            {
                result = result.ForceAbsoluteImgUrls();
            }
            catch (Exception e)
            {
                logger.LogError($"Unable to correct Absolute Urls in XhtmlString requested from {context?.Request?.GetDisplayUrl()}", e);
            }

            return result;
        }
    }
}