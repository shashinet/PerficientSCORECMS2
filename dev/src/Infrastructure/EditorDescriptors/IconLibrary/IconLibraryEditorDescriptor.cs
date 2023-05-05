using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;

namespace Perficient.Infrastructure.EditorDescriptors.IconLibrary
{
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = "IconLibraryEditor")]
    public class IconLibraryEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            SelectionFactoryType = typeof(IconLibrarySelectionFactory);
            ClientEditingClass = "custom-scripts/editors/iconlibraryeditor";
            base.ModifyMetadata(metadata, attributes);
        }
    }
}
