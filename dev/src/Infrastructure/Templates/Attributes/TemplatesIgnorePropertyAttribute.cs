using System;

namespace Perficient.Infrastructure.Templates.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TemplatesIgnorePropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the TemplatesIgnorePropertyAttribute class.
        /// </summary>
        public TemplatesIgnorePropertyAttribute() => this.IgnoreProperty = true;

        /// <summary>
        /// Initializes a new instance of the TemplatesIgnorePropertyAttribute class and sets the IgnoreProperty value.
        /// </summary>
        /// <param name="ignoreProperty">Sets the value of the IgnoreProperty property.</param>
        public TemplatesIgnorePropertyAttribute(bool ignoreProperty) => this.IgnoreProperty = ignoreProperty;

        /// <summary>
        /// Gets a value indicating whether the property decorated with this attribute is ignored
        /// </summary>
        /// <value>
        ///     <c>true</c> if the property decorated with this attribute is ignored in the process of content populating in the Template Library; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>Default value is <c>true</c>.</remarks>
        public bool IgnoreProperty { get; private set; }
    }
}
