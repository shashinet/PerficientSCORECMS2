using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perficient.Infrastructure.EditorDescriptors.Colors
{
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = "SolidColorsPickerEditor")]
    public class SolidColorsPickerEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            SelectionFactoryType = typeof(SolidColorsSelectionFactory);
            ClientEditingClass = "score/editors/colorPickerEditor";
            base.ModifyMetadata(metadata, attributes);
        }
    }
}
