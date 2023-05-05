using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.SelectionFactories
{
    public class QuickListSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            if (metadata.Attributes.FirstOrDefault(a => a.GetType() == typeof(QuickSelectAttribute)) is QuickSelectAttribute quickSelectAttr)
            {
                metadata.InitialValue = quickSelectAttr.QuickListItems.FirstOrDefault() ?? "";

                return quickSelectAttr.QuickListItems.Select(li => new SelectItem() { Text = li, Value = li });
            }

            return Enumerable.Empty<SelectItem>();
        }
    }
}
