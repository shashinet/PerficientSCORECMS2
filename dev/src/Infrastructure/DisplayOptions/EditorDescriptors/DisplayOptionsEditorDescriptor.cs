using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Perficient.Infrastructure.DisplayOptions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.DisplayOptions.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(EPiServer.Core.ContentArea))]
    public class DisplayOptionsEditorDescriptor : ContentAreaEditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            var displayOptions = attributes?.FirstOrDefault(a => a.GetType() == typeof(DisplayOptionsAttribute)) as DisplayOptionsAttribute;

            if (displayOptions != null)
            {
                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                };



                // need to set ClientEditingClass and OverlayConfiguration because ContentArea uses both for OPE or Properties view
                metadata.ClientEditingClass = "domanager-resources/editors/display/displayOptionsContentAreaEditor";
                metadata.OverlayConfiguration["customType"] = "domanager-resources/editors/display/displayOptionsContentAreaOverlay";
                metadata.OverlayConfiguration["displayOptions"] = JsonConvert.SerializeObject(displayOptions, settings);
                metadata.EditorConfiguration["displayOptions"] = JsonConvert.SerializeObject(displayOptions, settings);
            }
        }
    }
}
