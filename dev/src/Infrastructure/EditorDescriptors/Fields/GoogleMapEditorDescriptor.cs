using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Microsoft.Extensions.Configuration;
using Perficient.Infrastructure.Models.Properties;
using System;
using System.Collections.Generic;

namespace Perficient.Infrastructure.EditorDescriptors.Fields
{
    [EditorDescriptorRegistration(TargetType = typeof(GoogleMapCoordinates), UIHint = "GoogleMapEditor", EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault)]
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = "GoogleMapEditor", EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault)]
    public class GoogleMapEditorDescriptor : EditorDescriptor
    {
        private readonly Injected<IConfiguration> Configuration;

        public GoogleMapEditorDescriptor()
        {
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            ClientEditingClass = "score/editors/googleMapEditor";

            // API key for the Google Maps JavaScript API
            metadata.EditorConfiguration["apiKey"] = Configuration.Service["ScoreSettings:GoogleMaps:ApiKey"];

            // Default zoom level from 1 (least) to 20 (most)
            metadata.EditorConfiguration["defaultZoom"] = 12;

            // Default coordinates when no property value is set
            metadata.EditorConfiguration["defaultCoordinates"] = new { latitude = 34.0951529, longitude = -84.2584163 }; //45.2516454,19.8345265

            base.ModifyMetadata(metadata, attributes);
        }
    }
}
