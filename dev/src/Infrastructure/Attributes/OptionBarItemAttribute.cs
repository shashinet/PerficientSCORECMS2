using System;

namespace Perficient.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OptionBarItemAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets what the button text will be. If left blank, the Display Name will be used if available, followed by the Property name.
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Gets or sets whether this property should trigger a full refresh in On Page Editing when changed
        /// </summary>
        public bool TriggerFullRefresh { get; set; }

        /// <summary>
        /// Gets or sets whether the option bar will display the value for this item.
        /// </summary>
        public bool ShowFieldValue { get; set; }

        /// <summary>
        /// Gets or sets the order weight of the option in the option bar
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Gets or sets an item to display next to the text on the option bar
        /// </summary>
        public OptionBarItemAttribute()
        {
            TriggerFullRefresh = true;
        }
    }
}
