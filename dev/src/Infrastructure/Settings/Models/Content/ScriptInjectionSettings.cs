using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Newtonsoft.Json;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Models.Properties;
using Perficient.Infrastructure.Settings.Abstracts;
using Perficient.Infrastructure.Settings.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace erficient.Infrastructure.Settings.Models.Content
{

    [SettingsContentType(DisplayName = "Scripts Injection Settings",
        GUID = "0156b963-88a9-450b-867c-e5c5e7be29fd",
        Description = "Scripts Injection Settings",
        SettingsName = "Scripts Injection")]
    public class ScriptInjectionSettings : SettingsBase
    {
        #region Scripts

        [JsonIgnore]
        [CultureSpecific]
        [Display(Name = "Header Scripts (Scripts will inject at the bottom of header)",
            GroupName = TabNames.Scripts, Description = "Scripts will inject at the bottom of header",
            Order = 10)]
        [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<ScriptInjectionModel>))]
        public virtual IList<ScriptInjectionModel> HeaderScripts { get; set; }

        [JsonIgnore]
        [CultureSpecific]
        [Display(Name = "Footer Scripts (Scripts will inject at the bottom of footer)",
            GroupName = TabNames.Scripts, Description = "Scripts will inject at the bottom of footer",
            Order = 20)]
        [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<ScriptInjectionModel>))]
        public virtual IList<ScriptInjectionModel> FooterScripts { get; set; }

        #endregion
    }
}
