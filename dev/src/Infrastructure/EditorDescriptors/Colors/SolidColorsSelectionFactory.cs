using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perficient.Infrastructure.EditorDescriptors.Colors
{
    public class SolidColorsSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var scoreSettings = settingsService.GetSiteSettings<StyleSettings>();

            var settings = new List<SelectItem>();
            if (scoreSettings?.SolidColors != null)
                settings.AddRange(scoreSettings.SolidColors.Select(color => new SelectItem { Text = $"{color.Name}", Value = color.ColorCode }));

            return settings;
        }
    }
}
