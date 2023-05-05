using EPiServer;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using System;

namespace Perficient.Web.Middleware.ApiModelFilter
{
    [ServiceConfiguration(typeof(IContentApiModelFilter), Lifecycle = ServiceInstanceScope.Singleton)]
    public class CustomContentApiModelFilter : ContentApiModelFilter<ContentApiModel>
    {
        private readonly IContentLoader contentLoader;
        private readonly IContentTypeRepository contentTypeRepository;
        private const string CONTENT_TYPE_GUID = "ContentTypeGuid";

        public CustomContentApiModelFilter(IContentLoader contentLoader, IContentTypeRepository contentTypeRepository)
        {
            this.contentLoader = contentLoader;
            this.contentTypeRepository = contentTypeRepository;
        }

        public override void Filter(ContentApiModel contentApiModel, ConverterContext converterContext)
        {
            // To remove values from the output, set them to null.
            // In Startup.cs we have configured the ContentDeliveryApiSerializer to "remove" null values from the response:
            //      // in Startup.cs:
            //      services.ConfigureContentDeliveryApiSerializer(settings => {
            //          settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //      });
            // thus the response output will not contain these "out of the box" fields
            contentApiModel.StartPublish = null;
            contentApiModel.StopPublish = null;
            contentApiModel.ParentLink = null;
            contentApiModel.RouteSegment = null;
            contentApiModel.Created = null;
            contentApiModel.Saved = null;
            contentApiModel.Status = null;

            // remove category as we don't need it at the API level
            contentApiModel.Properties.Remove("Category");

            #region Add Content Type GUID to output
            // add a field called contentTypeGuid which has the ID of the content type in the output, this will be
            // useful for keying off of to understand what type of content is being delivered
            contentApiModel.Properties[CONTENT_TYPE_GUID] = Guid.Empty.ToString("N");
            if (contentApiModel.ContentLink?.Id != null && contentApiModel.ContentLink.Id.HasValue)
            {
                var content = contentLoader.Get<IContent>(new ContentReference(contentApiModel.ContentLink.Id.Value));
                var contentType = content.ContentTypeID;
                var type = contentTypeRepository.Load(contentType);

                if (type != null && type.GUID != Guid.Empty)
                {
                    contentApiModel.Properties[CONTENT_TYPE_GUID] = type.GUID.ToString("N");
                }
            }
            #endregion
        }
    }
}
