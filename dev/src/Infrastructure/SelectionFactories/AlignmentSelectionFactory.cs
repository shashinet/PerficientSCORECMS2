using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;

namespace Perficient.Infrastructure.SelectionFactories
{
    public class AlignmentSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                new SelectItem{Text = "Left", Value = "justify-start"},
                new SelectItem{Text = "Centered", Value = "justify-center"},
                new SelectItem{Text = "Right", Value = "justify-end"},
                new SelectItem{Text = "Center Fixed", Value = "center-fixed"}
            };
        }
    }
}
