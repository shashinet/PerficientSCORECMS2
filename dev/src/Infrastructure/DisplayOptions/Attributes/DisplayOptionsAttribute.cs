using System;

namespace Perficient.Infrastructure.DisplayOptions.Attributes
{
    /// <summary>
    /// Specify DisplayOptions available for a ContentArea, Page, or Block type, or specify False to disable DisplayOptions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DisplayOptionsAttribute : Attribute
    {
        /// <summary>
        /// Provide an array of display option names to control which options are available for a property or type.
        /// </summary>
        public string[] DisplayOptions { get; private set; } = Array.Empty<string>();

        /// <summary>
        /// Specify false to disable display options for a property or type
        /// </summary>
        public bool ShowDisplayOptions { get; private set; } = true;

        /// <summary>
        /// Specify to control whether DisplayOptions selection is available to this block type
        /// </summary>
        /// <param name="displayOptionName">Specify specific options to control which ones are available to a given block.</param>
        public DisplayOptionsAttribute(params string[] displayOptionName)
        {
            DisplayOptions = displayOptionName;
        }

        public DisplayOptionsAttribute(bool disableOptions)
        {
            ShowDisplayOptions = disableOptions;
        }

        /// <summary>
        /// Specify to control whether DisplayOptions selection is available to this block type
        /// </summary>
        public DisplayOptionsAttribute()
        {
        }
    }
}
