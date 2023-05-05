using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.EditorDescriptors.Picture
{
    [EditorDescriptorRegistration(
        TargetType = typeof(string), 
        UIHint = "ResponsivePictureEditor", 
        EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault)]
    public class ResponsivePictureEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            ClientEditingClass = "score/editors/picture/main";

            if (metadata != null)
            {
                var croppings = metadata.Attributes.OfType<CropPointAttribute>();
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                metadata.EditorConfiguration["devices"] = JsonConvert.SerializeObject(croppings, serializerSettings);
                metadata.AdditionalValues["DropTargetType"] = new[] { "imageurl" };
            }

            base.ModifyMetadata(metadata, attributes);
        }
    }
}
