using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Logging;
using EPiServer.Security;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Perficient.Infrastructure.Extensions;
using Perficient.Infrastructure.Interfaces.Services;
using Perficient.Infrastructure.Models.Properties;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Perficient.Infrastructure.Services
{
    public class StyleSettingsImportService : IStyleSettingsImportService
    {
        private readonly IContentRepository _contentRepository;
        private readonly ContentRootRepository _contentRootRepository;
        private readonly IConfiguration _configuration;
        private readonly ISettingsService _settingsService;

        private static readonly ILogger _logger = LogManager.GetLogger(typeof(StyleSettingsImportService));
        public const string SettingsRootName = "SettingsRoot";

        public StyleSettingsImportService(IContentRepository contentRepository,
            ContentRootRepository contentRootRepository,
            IConfiguration config,
            ISettingsService settingsService)
        {
            _logger.Information("Initializing StyleSettingsImportService");
            _contentRepository = contentRepository;
            _contentRootRepository = contentRootRepository;
            _configuration = config;
            _settingsService = settingsService;
        }

        public void UpdateStyleSettings()
        {
            var sitesList = GetSitesList();
            _logger.Information($"StyleSettingsImportService - Site Count: {sitesList.Count()}");
            foreach (var site in sitesList)
            {
                var styleSettingsFromJsonFile = LoadSiteStyleSettingsFromJson(site.Name);

                if (styleSettingsFromJsonFile == null) continue;

                var localStyleSettingsPage = _contentRepository.GetChildren<IContent>(site.ContentLink).Where(s => s.Name == "Style Settings")?.First() as StyleSettings;

                if (localStyleSettingsPage != null) 
                {
                    CopyStyleSettingsFromJson(site.Name, styleSettingsFromJsonFile, localStyleSettingsPage);
                }
            }
        }

        private IEnumerable<IContent> GetSitesList()
        {
            //Need to make sure that the Root folders are registered
            _settingsService.RegisterContentRoots();
            var registeredRoots = _contentRootRepository.List();
            _logger.Information($"StyleSettingsImportService - Registered Roots: {string.Join(", ", registeredRoots.Keys)}");
            var settingsRootRef = registeredRoots.FirstOrDefault(x => x.Key.Equals(SettingsFolder.SettingsRootName));

            _logger.Information($"StyleSettingsImportService - Settings Root Ref: {settingsRootRef.Value?.ID}");
            return _contentRepository.GetChildren<IContent>(settingsRootRef.Value).ToList();
        }

        private StyleSettings LoadSiteStyleSettingsFromJson(string siteName)
        {
            var jsonSettings = JsonConvert.DeserializeObject<StyleSettings>(_configuration[$"{siteName}.json"] ?? string.Empty);
            if (jsonSettings == null)
            {
                {
                    _logger.Information($"StyleSettingsImportService - Error processing Settings Json file for {siteName}.");
                }
            }
            return jsonSettings;
        }

        private void CopyStyleSettingsFromJson(string siteName, StyleSettings styleSettingsFromJsonFile, StyleSettings localStyleSettings)
        {
            var clone = localStyleSettings.CreateWritableClone() as StyleSettings;

            clone = CompareSource(clone, styleSettingsFromJsonFile);

            try
            {
                _contentRepository.Save(clone, SaveAction.Publish, AccessLevel.NoAccess);
            }
            catch (Exception ex)
            {
                _logger.Error($"StyleSettingsImportService - Error processing copying settings from  Json file for {siteName}.", ex);
            }
        }

        private StyleSettings CompareSource(StyleSettings baseSettings, StyleSettings jsonSettings)
        {
            var settingsType = jsonSettings.GetType();
            foreach (var setting in baseSettings.Property)
            {
                var propType = settingsType.GetProperty(setting.Name);
                if (propType == null) continue;

                var propValue = propType.GetValue(jsonSettings);
                if (propValue == null) continue;


                var mergedClasses = MergeStyles(setting, propValue);

                var mergedClassesEnumerator = (mergedClasses as IEnumerable).GetEnumerator();
                mergedClassesEnumerator.MoveNext();
                var untypedClass = mergedClassesEnumerator.Current;

                dynamic typedClasses= null;
                switch (untypedClass)
                {
                    case ScoreColor:
                        typedClasses = (mergedClasses as IEnumerable).Cast<ScoreColor>();
                        break;
                    case ScoreButtonStyle:
                        typedClasses = (mergedClasses as IEnumerable).Cast<ScoreButtonStyle>();
                        break;
                    case ScoreClass:
                        typedClasses = (mergedClasses as IEnumerable).Cast<ScoreClass>();
                        break;
                    case ScoreButtonSize:
                        typedClasses = (mergedClasses as IEnumerable).Cast<ScoreButtonSize>();
                        break;
                    default:
                        break;

                }

                setting.Value = typedClasses;
            }

            return baseSettings;
        }

        private T MergeStyles<T>(PropertyData baseClassProp, T propValue)
        {
            var baseClasses = baseClassProp.Value as IEnumerable<object> ?? new List<object>();

            var jsonClasses = propValue as IEnumerable<object>;

            dynamic diffs = new List<object>();

            foreach (var jsonClass in jsonClasses)
            {
                bool isFound = false;
                foreach (var baseClass in baseClasses)
                {
                    if (baseClass.Equals(jsonClass))
                    {
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                {
                    diffs.Add(jsonClass);
                }
            }

            diffs.AddRange(baseClasses);

            return (T)diffs;
        }
    }
}
