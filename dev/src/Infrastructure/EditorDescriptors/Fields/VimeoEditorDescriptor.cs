using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Microsoft.Extensions.Configuration;
using Perficient.Infrastructure.Models.Properties;
using System;
using System.Collections.Generic;

namespace Perficient.Infrastructure.EditorDescriptors.Fields
{
    [EditorDescriptorRegistration(TargetType = typeof(ScoreVideo), UIHint = "VimeoVideoEditor", EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault)]
    public class VimeoEditorDescriptor : EditorDescriptor
    {
        private readonly Injected<IConfiguration> Configuration;

        public VimeoEditorDescriptor()
        {
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            ClientEditingClass = "score/editors/vimeoVideoEditor";

            // API key for the YouTube JavaScript API
            metadata.EditorConfiguration["apiKey"] = Configuration.Service["ScoreSettings:Vimeo:ApiKey"]; ;
            base.ModifyMetadata(metadata, attributes);
        }
    }
}
