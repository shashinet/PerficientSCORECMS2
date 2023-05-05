using DoubleJay.Epi.EnhancedPropertyList.EditorDescriptors;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.Models.Properties;
using Perficient.Infrastructure.Settings.Abstracts;
using Perficient.Infrastructure.Settings.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Settings.Models.Content
{
    [SettingsContentType(
       DisplayName = "Icon Library Settings",
       GUID = "ecb03080-472e-4e72-9ceb-c56e775b84d6",
       Description = "Icon Library Settings",
       SettingsName = "Icon Library Settings",
       AvailableInEditMode = true)]
    [ImageUrl("~/icons/cms/pages/cms-icon-page-layout-settings.png")]
    public class IconLibrarySettings : SettingsBase
    {
        [Display(Name = "Icons")]
        [EditorDescriptor(EditorDescriptorType = typeof(EnhancedCollectionEditorDescriptor<IconLibrary>))]
        public virtual IList<IconLibrary> Icons { get; set; }
    }
}
