using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.DynamicProperties.Models
{
    public class DynamicPropertyRegistratorModel
    {
        public DynamicPropertyRegistratorModel(string dynamicPropertyName)
        {
            DynamicProperty = dynamicPropertyName;
        }

        public string DynamicProperty { get; set; }

        public Dictionary<string, string[]> HideFields { get; set; } = new Dictionary<string, string[]>();

        public Dictionary<string, string[]> ShowFields { get; set; } = new Dictionary<string, string[]>();

        public void PrependValueToPropertyNames(string prependValue)
        {
            DynamicProperty = $"{prependValue}.{DynamicProperty}";
            var localHideFields = new Dictionary<string, string[]>();
            var localShowFields = new Dictionary<string, string[]>();

            foreach (var hideField in HideFields)
            {
                localHideFields.Add(hideField.Key.ToLower(), hideField.Value.Select(x => $"{prependValue}.{x}").ToArray());
            }

            foreach (var showField in ShowFields)
            {
                localShowFields.Add(showField.Key.ToLower(), showField.Value.Select(x => $"{prependValue}.{x}").ToArray());
            }

            HideFields = localHideFields;
            ShowFields = localShowFields;
        }

        public void ForceEverythingLowercase()
        {
            DynamicProperty = DynamicProperty.ToLower();
            var localHideFields = new Dictionary<string, string[]>();
            var localShowFields = new Dictionary<string, string[]>();

            foreach (var hideField in HideFields)
            {
                localHideFields.Add(hideField.Key.ToLower(), hideField.Value.Select(x => x.ToLower()).ToArray());
            }

            foreach (var showField in ShowFields)
            {
                localShowFields.Add(showField.Key.ToLower(), showField.Value.Select(x => x.ToLower()).ToArray());
            }

            HideFields = localHideFields;
            ShowFields = localShowFields;
        }
    }
}
