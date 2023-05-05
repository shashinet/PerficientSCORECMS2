using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web.Routing;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.EditorDescriptors.IconLibrary
{
    public class IconLibrarySelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();
            var iconSettings = settingsService.GetSiteSettings<IconLibrarySettings>();

            var settings = new List<ISelectItem>();
            if (iconSettings?.Icons != null)
                settings.AddRange(iconSettings.Icons.Select(i => new SelectItem { Text = $"{i.IconName}", Value = urlResolver.GetUrl(i.IconMedia) }));

            return settings;
        }
    }
}
