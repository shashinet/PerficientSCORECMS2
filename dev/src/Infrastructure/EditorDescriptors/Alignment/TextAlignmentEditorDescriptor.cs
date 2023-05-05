using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;
using Perficient.Infrastructure.SelectionFactories;

namespace Perficient.Infrastructure.EditorDescriptors.Alignment
{
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = "TextAlignmentEditor")]
    public class TextAlignmentEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            SelectionFactoryType = typeof(TextAlignmentSelectionFactory);
            ClientEditingClass = "score/editors/alignmentEditor";

            base.ModifyMetadata(metadata, attributes);
        }
    }
}
