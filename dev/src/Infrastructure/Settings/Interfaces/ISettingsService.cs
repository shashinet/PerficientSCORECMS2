using EPiServer.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;

namespace Perficient.Infrastructure.Settings.Interfaces
{
    public interface ISettingsService
    {
        ContentReference GlobalSettingsRoot { get; set; }
        ConcurrentDictionary<string, Dictionary<Type, object>> SiteSettings { get; }
        T GetSiteSettings<T>(Guid? siteId = null);
        T GetSiteSettings<T>(CultureInfo preferredLanguage, Guid? siteId = null);
        void InitializeSettings();
        void RegisterContentRoots();

        void UnintializeSettings();
        void UpdateSettings(Guid siteId, IContent content, bool isContentNotPublished);
        void UpdateSettings();

        
    }
}
