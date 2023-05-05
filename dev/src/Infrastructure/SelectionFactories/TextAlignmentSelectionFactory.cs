using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;

namespace Perficient.Infrastructure.SelectionFactories
{
    public class TextAlignmentSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                new SelectItem{Text = "Text Left", Value = "text-left"},
                new SelectItem{Text = "Text Centered", Value = "text-center"},
                new SelectItem{Text = "Text Right", Value = "text-right"}
            };
        }
    }
}
