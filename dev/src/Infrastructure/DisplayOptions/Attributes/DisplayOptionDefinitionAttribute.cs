using System;

namespace Perficient.Infrastructure.DisplayOptions.Attributes
{
    /// <summary>
    /// Indicate a constant field is a DisplayOptionDefinition. The field value is used as the name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class DisplayOptionDefinitionAttribute : Attribute
    {
        /// <summary>
        /// The CSS class applied to the menu option to show an icon
        /// </summary>
        public string IconClass { get; set; }
        /// <summary>
        /// The tag used for rendering a block using this DisplayOption
        /// </summary>
        public string RenderingTag { get; set; }
        /// <summary>
        /// Indicate the order you want this option appearing in the menu
        /// </summary>
        public int Order { get; set; }

        public DisplayOptionDefinitionAttribute() { }

        public DisplayOptionDefinitionAttribute(string iconClass, string renderingTag, int order)
        {
            this.IconClass = iconClass;
            this.RenderingTag = renderingTag;
            this.Order = order;
        }
    }
}
