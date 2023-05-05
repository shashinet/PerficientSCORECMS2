using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.EditorDescriptors.Style
{
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = "StyleEditor", EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault)]
    public class StyleEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            ClientEditingClass = "score/editors/style/main";

            if (metadata != null)
            {
                var property = metadata.Attributes.OfType<ClassSelectionsAttribute>().FirstOrDefault();

                if (property != null)
                {
                    var settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
                    var scoreSettings = settingsService.GetSiteSettings<StyleSettings>();

                    metadata.EditorConfiguration["selections"] = scoreSettings?.GetType()?.GetProperty(property.SettingsPropertyName)?.GetValue(scoreSettings, null);
                }
            }

            base.ModifyMetadata(metadata, attributes);
        }
    }
}
