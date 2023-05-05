using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;

namespace Perficient.Infrastructure.EditorDescriptors.Layout
{
    public class PageLayoutSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                new SelectItem { Text = "1 Column", Value = "1c" },
                new SelectItem { Text = "1 Column Wide", Value = "1cw" },
                new SelectItem { Text = "2 Column Equal", Value = "2ce" },
                new SelectItem { Text = "2 Column Large Right", Value = "2clr" },
                new SelectItem { Text = "2 Column Large Left", Value = "2cll" },
                new SelectItem { Text = "3 Column Equal", Value = "3ce" },
                new SelectItem { Text = "3 Column Large Middle", Value = "3clm" },
                new SelectItem { Text = "4 Column Equal", Value = "4ce" }
            };
        }
    }
}
