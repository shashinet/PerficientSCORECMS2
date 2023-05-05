using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;

namespace Perficient.Infrastructure.EditorDescriptors.Colors
{
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = "ColorPickerEditor")]
    public class ColorPickerEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            SelectionFactoryType = typeof(ColorSelectionFactory);
            ClientEditingClass = "score/editors/colorPickerEditor";
            base.ModifyMetadata(metadata, attributes);
        }
    }
}
