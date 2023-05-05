//using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
//using EPiServer.Shell.ObjectEditing;
//using EPiServer.Shell.ObjectEditing.EditorDescriptors;
//using System;
//using System.Collections.Generic;


//namespace Perficient.Infrastructure.EditorDescriptors.ContentArea
//{
//    [EditorDescriptorRegistration(TargetType = typeof(EPiServer.Core.ContentArea), EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault)]
//    public class ContentAreaMenuEditDescriptor : ContentAreaEditorDescriptor
//    {
//        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
//        {
//            base.ModifyMetadata(metadata, attributes);

//            metadata.OverlayConfiguration["customType"] = "score/editors/util/editorEditMenuActions";
//        }
//    }
//}
