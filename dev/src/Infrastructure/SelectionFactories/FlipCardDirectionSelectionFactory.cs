using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perficient.Infrastructure.SelectionFactories
{
    public class FlipCardDirectionSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {

            var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var scoreSettings = settingsService.GetSiteSettings<StyleSettings>();

            var settings = new List<SelectItem>();
            if (scoreSettings?.FlipCardDirectionClasses != null)
                settings.AddRange(scoreSettings.FlipCardDirectionClasses.Select(x => new SelectItem { Text = $"{x.Name}", Value = x.ScoreClassName }));

            return settings;
        }
    }
}
