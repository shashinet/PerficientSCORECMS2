using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.EditorDescriptors.Colors
{
    public class ColorSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var scoreSettings = settingsService.GetSiteSettings<StyleSettings>();

            var settings = new List<SelectItem>();
            if (scoreSettings?.Colors != null)
                settings.AddRange(scoreSettings.Colors.Select(color => new SelectItem { Text = $"{color.Name}", Value = color.ColorCode }));

            return settings;
        }
    }
}