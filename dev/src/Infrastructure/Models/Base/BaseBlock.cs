using System.ComponentModel.DataAnnotations;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;

using Newtonsoft.Json;

using Perficient.Infrastructure.Attributes;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.EditorDescriptors.Style;
using Perficient.Infrastructure.Templates.Interfaces;

namespace Perficient.Infrastructure.Models.Base
{
    public abstract class BaseBlock : BlockData, ITemplateBlock
    {
        // always display colors, then local styles, then global styles
        [Display(
            Name = "Global Styles",
            GroupName = TabNames.Styles,
            Order = 99)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(StyleEditorDescriptor))]
        [ClassSelections("StyleClasses")] // name of the class selection property on ScoreSettingsPage
        public virtual string GlobalStyle { get; set; }

        // TODO: will consider showing this property in Edit Mode
        //[Display(GroupName = TabNames.TemplateSettings, Name = "Selected Template", Order = 10)]
        [AllowedTypes(typeof(ITemplateBlock))]
        [ScaffoldColumn(false)]
        public virtual ContentReference SelectedTemplate { get; set; }

        [AllowedTypes(typeof(ITemplateBlock))]
        [ScaffoldColumn(false)]
        public virtual ContentReference OldTemplate { get; set; }

        [Display(
            Name = "Index In Content Areas",
            GroupName = TabNames.Settings,
            Order = 99)]
        public virtual bool IndexInContentAreas { get; set; }

        public virtual string GetClassList() => string.IsNullOrWhiteSpace(GlobalStyle) ? string.Empty : $" {GlobalStyle.Replace(",", " ")}";

        public virtual string GetClassList(string selections)
        {
            var classes = string.Empty;

            if (!string.IsNullOrWhiteSpace(GlobalStyle))
            {
                classes += $"{GlobalStyle.Replace(",", " ")}";
            }

            classes += string.IsNullOrWhiteSpace(selections) ? " default" : $" {selections.Replace(",", " ")}";

            while (classes.Contains("  "))
            {
                classes = classes.Replace("  ", " ");
            }

            return classes.Trim();
        }

        [JsonIgnore]
        public virtual string Id
        {
            get
            {
                var id = (this as IContent).ContentLink.ID;

                return $"block-{id}";
            }
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            IndexInContentAreas = true;
        }
    }
}