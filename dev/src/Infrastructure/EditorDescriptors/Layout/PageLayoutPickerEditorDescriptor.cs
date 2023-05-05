using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;

namespace Perficient.Infrastructure.EditorDescriptors.Layout
{
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = "LayoutPickerEditor")]
    public class PageLayoutPickerEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            SelectionFactoryType = typeof(PageLayoutSelectionFactory);
            ClientEditingClass = "score/editors/pagelayoutpickereditor";

            base.ModifyMetadata(metadata, attributes);
        }
    }
}
