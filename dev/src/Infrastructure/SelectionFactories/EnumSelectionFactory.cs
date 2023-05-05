using EPiServer.Framework.Localization;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace Perficient.Infrastructure.SelectionFactories
{
    public class EnumSelectionFactory<TEnum> : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var enumValues = Enum.GetValues(typeof(TEnum));

            foreach (var value in enumValues)
            {
                yield return new SelectItem
                {
                    Text = GetDisplayText(value),
                    Value = value
                };
            }
        }

        private static string GetDisplayText(object value)
        {
            var displayName = Enum.GetName(typeof(TEnum), value);

            string localizationPath = string.Format("/properties/enum/{0}/{1}", typeof(TEnum).Name.ToLowerInvariant(), displayName.ToLowerInvariant());

            if (LocalizationService.Current.TryGetString(localizationPath, out string localizedName))
            {
                return localizedName;
            }

            displayName = ((Enum)value).GetDisplay()?.Description ?? value.ToString();

            if (!string.IsNullOrWhiteSpace(displayName))
            {
                return displayName;
            }

            return Enum.GetName(typeof(TEnum), value);
        }
    }
}
