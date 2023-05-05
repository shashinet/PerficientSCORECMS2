using EPiServer;
using EPiServer.Cms.Shell.UI.Rest.Models.Internal;
using EPiServer.Core;
using EPiServer.Framework.Localization;
using EPiServer.Logging;
using EPiServer.Shell.Services.Rest;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Perficient.Infrastructure.DisplayOptions.Controllers
{

    [ApiController]
    [Route("episerver/cms/customstores/displayoptions")]
    public class DisplayOptionsController : ControllerBase
    {
        private readonly EPiServer.Web.DisplayOptions _displayOptions;
        private readonly LocalizationService _localizationService;
        private readonly IContentLoader _contentLoader;
        private readonly ILogger _logger = LogManager.GetLogger(typeof(DisplayOptionsController));

        public DisplayOptionsController(
            EPiServer.Web.DisplayOptions displayOptions,
            LocalizationService localizationService,
            IContentLoader contentLoader)
        {
            _displayOptions = displayOptions;
            _localizationService = localizationService;
            _contentLoader = contentLoader;
        }

        [HttpGet]
        [Route("")]

        public RestResult GetAllDisplayOptions()
        {
            var displayOptionsResult = _displayOptions.Select(d => new DisplayOptionModel(d, _localizationService));

            return new RestResult { Data = displayOptionsResult };
        }

        [HttpGet]
        [Route("availableoptions/{id}/{availableOptions?}")]
        public RestResult GetDisplayOptions(string id, string availableOptions)
        {
            _logger.Debug($"[DisplayOptionsController]:[GetDisplayOptions] - Getting Display Options for Content Area ID: {id}. Available Options: {availableOptions}.");

            return getDisplayOptionsResult(id, availableOptions?.Split(','));
        }

        [HttpGet]
        [Route("getdisplayoption/{id?}")]
        public RestResult GetDisplayOption(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var displayOption = _displayOptions.Get(id);

                return new RestResult { Data = displayOption };
            }

            return new RestResult { Data = null };
        }

        private RestResult getDisplayOptionsResult(string id, string[] availableOptions)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.Debug($"[DisplayOptionsController]:[getDisplayOptionsResult] - No ID Provided.");

                return GetAllDisplayOptions();
            }
            var displayOptionsResult = getDisplayOptionsList(id, availableOptions);
            if (displayOptionsResult == null)   // this happens when options are disabled.
            {
                _logger.Debug($"[DisplayOptionsController]:[getDisplayOptionsResult] - displayOptionsResult is null. Options are disabled.");

                return new RestResult { Data = new[] { "disabled" } };
            }

            if (!displayOptionsResult.Any())
            {
                _logger.Debug($"[DisplayOptionsController]:[getDisplayOptionsResult] - displayOptionsResult has no count. Getting all display options.");

                return GetAllDisplayOptions();
            }

            return new RestResult { Data = displayOptionsResult };
        }

        private IEnumerable<DisplayOptionModel> getDisplayOptionsList(string id, string[] availableOptions)
        {
            _logger.Debug($"[DisplayOptionsController]:[getDisplayOptionsList] - ID: {id}.");
            var contentReference = ContentReference.Parse(id);

            var contentItem = _contentLoader.Get<IContent>(contentReference);
            _logger.Debug($"[DisplayOptionsController]:[getDisplayOptionsList] - Content Item Found: {contentItem?.ContentGuid}.");

            var displayOptions = contentItem.GetOriginalType().GetCustomAttribute(typeof(DisplayOptionsAttribute)) as DisplayOptionsAttribute;
            if (displayOptions == null)
            {
                _logger.Debug($"[DisplayOptionsController]:[getDisplayOptionsList] - Display Options Attribute not found on Content Area: Content Item: {contentItem?.ContentGuid}.");

                return Enumerable.Empty<DisplayOptionModel>();
            }

            if (!displayOptions.ShowDisplayOptions)
            {
                _logger.Debug($"[DisplayOptionsController]:[getDisplayOptionsList] - Display Options Show Display Options is set to false.");

                return Enumerable.Empty<DisplayOptionModel>();
            }

            _logger.Debug($"[DisplayOptionsController]:[getDisplayOptionsList] - Display Options Show Display Options is set to true.");

            var selectedOptions = _displayOptions.Where(d => displayOptions.DisplayOptions.Contains(d.Name));
            if (availableOptions?.FirstOrDefault() != null)
            {
                selectedOptions = selectedOptions.Where(d => availableOptions.Any(o => string.Equals(o, d.Name, StringComparison.InvariantCultureIgnoreCase)));
            }
            _logger.Debug($"[DisplayOptionsController]:[getDisplayOptionsList] - Selected Options Count: {selectedOptions?.Count()}.");

            return selectedOptions.Select(d => new DisplayOptionModel(d, _localizationService)).ToList();
        }
    }
}
