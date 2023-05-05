using EPiServer.Core;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Models.Properties
{
    public class IconLibrary
    {
        [Display(Name = "Icon Name")]
        public string IconName { get; set; }
        [Display(Name = "Icon File")]
        [UIHint(UIHint.Image)]
        public ContentReference IconMedia { get; set; }

        [Display(Name = "Use as default icon")]
        public bool UseAsDefault { get; set; }
    }
}
