using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Perficient.Infrastructure.EditorDescriptors.ContentCreate
{
    public class ContentCreateMetadataExtender : IMetadataExtender
    {
        public void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            // When content is being created the content link is 0
            if (metadata.Model is IContent data && data.ContentLink.ID == 0)
            {
                try
                {
                    foreach (var modelMetadata in metadata?.Properties)
                    {
                        var property = (ExtendedMetadata)modelMetadata;
                        // The content is being created, so set required = false
                        if (property.Attributes.OfType<HideOnContentCreateAttribute>().Any())
                        {
                            property.IsRequired = false;
                            property.ShowForEdit = false;
                        }
                    }
                }
                catch { }
            }
        }
    }
}
