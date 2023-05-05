using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Templates.Interfaces;
using Perficient.Infrastructure.Templates.Models;
using Perficient.Infrastructure.Templates.Services;

namespace Perficient.Infrastructure.Templates.Controllers
{
    [ApiController]
    [Route("api/template")]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplatesService _templatesService;
        private readonly IContentLoader _contentLoader;
        private readonly IContentTypeRepository _contentTypeRepository;

        public TemplatesController(
            ITemplatesService templatesService,
            IContentLoader contentLoader,
            IContentTypeRepository contentTypeRepository)
        {
            _templatesService = templatesService;
            _contentLoader = contentLoader;
            _contentTypeRepository = contentTypeRepository;
        }

        [Route("save")]
        [HttpPost]
        [Authorize]
        public IActionResult SaveAsTemplate(int contentId, int contentTypeId, int folderId)
        {
            var templateId = _templatesService.SaveAsTemplate(contentId, contentTypeId, folderId, out string message);

            if (templateId == 0)
            {
                return BadRequest(message);
            }

            return Ok(templateId);
        }

        [Route("interfaceImplemented")]
        [HttpGet]
        [Authorize]
        public IActionResult InterfaceImplemented(int contentTypeId)
        {
            // check if the content type from source exist
            var contentType = _contentTypeRepository.Load(contentTypeId);

            if (contentType == null)
            {
                return BadRequest("Content Type Not Existed");
            }

            var isImplemented = typeof(ITemplateContent).IsAssignableFrom(contentType.ModelType);

            return Ok(isImplemented);
        }

        [Route("getBaseType")]
        [HttpGet]
        [Authorize]
        public IActionResult GetBaseType(int contentTypeId)
        {
            // check if the content type from source exist
            var contentType = _contentTypeRepository.Load(contentTypeId);

            if (contentType == null)
            {
                return BadRequest("Content Type Not Existed");
            }

            if (typeof(BlockData).IsAssignableFrom(contentType.ModelType))
            {
                return Ok("block");
            }
            else if (typeof(PageData).IsAssignableFrom(contentType.ModelType))
            {
                return Ok("page");
            }

            return BadRequest("Content Type is not Block Type or Page Type");
        }

        [Route("getTemplateRoot")]
        [HttpGet]
        [Authorize]
        public IActionResult GetTemplateRoot()
        {
            var templateRoot = _contentLoader.Get<TemplatesRootFolder>(TemplatesRootFolder.TemplatesRootGuid);

            if (templateRoot != null)
            {
                return Ok(templateRoot.ContentLink.ID.ToString());
            }

            return Ok("0");
        }

        [Route("isSupported")]
        [HttpGet]
        [Authorize]
        public IActionResult IsContentIdSupported(int id)
        {
            if (_contentLoader.TryGet(new ContentReference(id), out ITemplateContent _))
            {
                return Ok(true);
            }

            return Ok(false);
        }

        [Route("getContentTypeAssemblyName")]
        [HttpGet]
        [Authorize]
        public IActionResult GetContentTypeAssemblyName(int contentId)
        {
            if (_contentLoader.TryGet(new ContentReference(contentId), out IContent content))
            {
                var contentType = _contentTypeRepository.Load(content.ContentTypeID);
                if (contentType != null)
                {
                    return Ok(contentType.ModelTypeString?.Split(',')[0].ToLower());
                }
            }

            return Ok(string.Empty);
        }
    }
}
