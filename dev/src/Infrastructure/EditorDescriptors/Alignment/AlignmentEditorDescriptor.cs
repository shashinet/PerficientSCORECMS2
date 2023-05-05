using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;
using Perficient.Infrastructure.SelectionFactories;

namespace Perficient.Infrastructure.EditorDescriptors.Alignment
{
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = "AlignmentEditor")]
    public class AlignmentEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            SelectionFactoryType = typeof(AlignmentSelectionFactory);
            ClientEditingClass = "score/editors/alignmentEditor";

            base.ModifyMetadata(metadata, attributes);
        }
    }
}
