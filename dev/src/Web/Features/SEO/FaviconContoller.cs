using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.Blobs;
using EPiServer.Logging;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Settings.Interfaces;
using System;

namespace Perficient.Web.Features.SEO
{
    public class FaviconContoller : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly IBlobFactory _blobFactory;
        private readonly IContentRepository _contentRepository;

        public FaviconContoller(ISettingsService settingsService, IBlobFactory blobFactory, IContentRepository contentRepository)
        {
            _settingsService = settingsService;
            _blobFactory = blobFactory;
            _contentRepository = contentRepository;
        }

        [Route("favicon.ico")]
        public ActionResult Index()
        {
            var iconReference = _settingsService.GetSiteSettings<Perficient.Infrastructure.Settings.Models.Content.SiteSettings>()?.Favicon;
            if (iconReference != null && iconReference != ContentReference.EmptyReference)
            {
                var image = _contentRepository.Get<ImageData>(iconReference);
                try
                {
                    var imageBytes = _blobFactory.GetBlob(image.BinaryData.ID).ReadAllBytes();
                    return File(imageBytes, "images/x-icon", "favicon.ico");
                }
                catch (Exception ex)
                {
                    var logger = LogManager.GetLogger(typeof(FaviconContoller));
                    logger.Error("Favicon error.", ex);
                }
            }
            return File("~/icons/favicon.ico", "images/x-icon");
        }
    }
}
