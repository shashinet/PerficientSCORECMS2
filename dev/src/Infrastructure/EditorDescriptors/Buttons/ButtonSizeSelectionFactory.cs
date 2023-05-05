using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.EditorDescriptors.Buttons
{
    public class ButtonSizeSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var scoreSettings = settingsService.GetSiteSettings<StyleSettings>();

            var settings = new List<SelectItem>();
            if (scoreSettings?.ButtonSizes != null)
                settings.AddRange(scoreSettings.ButtonSizes.Select(size => new SelectItem { Text = $"{size.Name}", Value = size.ButtonSizeClass }));

            return settings;
        }
    }
}
