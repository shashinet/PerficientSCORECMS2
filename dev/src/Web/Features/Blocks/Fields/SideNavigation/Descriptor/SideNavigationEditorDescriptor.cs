using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Perficient.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Web.Features.Blocks.Fields.SideNavigation.Descriptor
{
    [EditorDescriptorRegistration(TargetType = typeof(SideNavigationBlock))]
    public class SideNavigationEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            // Extends base opti widget by adding an event to hide the content area
            // if the editor opts for an Auto-Generated navigation type
            ClientEditingClass = "score/editors/sideNavigationEditor";

            // Hide block completely from CMS editors if ParentMenuContentTypeAttribute is applied
            // as it programmatically generates the side navigation menu
            if (attributes?.FirstOrDefault(x => x.GetType() == typeof(ParentMenuContentTypeAttribute))
                is ParentMenuContentTypeAttribute parentMenuContentType)
            {
                metadata.ShowForEdit = false;
            }

            base.ModifyMetadata(metadata, attributes);
        }
    }

}
