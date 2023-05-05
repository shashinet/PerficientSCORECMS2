using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;

namespace Perficient.Infrastructure.EditorDescriptors.Themes
{
    public class SiteThemeSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                new SelectItem { Text = "Perficient", Value = "Perficient" },
                new SelectItem { Text = "Secondary", Value = "Secondary" }
            };
        }
    }
}
