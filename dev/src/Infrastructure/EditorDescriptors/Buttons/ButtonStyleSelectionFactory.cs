using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.EditorDescriptors.Buttons
{
    public class ButtonStyleSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var scoreSettings = settingsService.GetSiteSettings<StyleSettings>();
            var settings = new List<SelectItem>();
            if (scoreSettings?.ButtonStyles != null)
                settings.AddRange(scoreSettings.ButtonStyles.Select(style => new SelectItem { Text = $"{style.Name}", Value = style.ButtonStyleClass }));

            return settings;
        }
    }
}