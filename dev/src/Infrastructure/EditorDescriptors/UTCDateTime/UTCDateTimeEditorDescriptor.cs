using System;
using System.Collections.Generic;
using System.Linq;

using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Extensions;

namespace Perficient.Infrastructure.EditorDescriptors.UTCDateTime
{
    [EditorDescriptorRegistration(TargetType = typeof(DateTime?), UIHint = "UTC")]
    [EditorDescriptorRegistration(TargetType = typeof(DateTime), UIHint = "UTC")]
    public class UTCDateTimeEditorDescriptor : DateTimeEditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            ClientEditingClass = "score/editors/utcDateTimeTextBox";

            var dateTimeType = attributes.OfType<DateTimeTypeAttribute>()?.FirstOrDefault();
            if (dateTimeType != null)
            {
                metadata.EditorConfiguration.Add("selector", dateTimeType.Selector.GetDisplayText());
            }
        }
    }
}
