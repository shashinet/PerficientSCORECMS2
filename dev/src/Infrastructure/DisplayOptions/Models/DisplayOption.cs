namespace Perficient.Infrastructure.DisplayOptions.Models
{
    public class DisplayOption
    {
        /// <summary>
        /// A string used to identify this DisplayOption in Episerver and for the select menu
        /// </summary>
        public string Name { get; set; }
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
    }
}
