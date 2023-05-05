using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Definitions
{
    public enum SiteTemplate
    {
        //The int value for the enum is used to set the Site Template
        [Display(Description = "Perficient", Name = "Perficient")]
        Perficient = 1,

        [Display(Description = "Secondary site template", Name = "Secondary")]
        Secondary = 2,
    }
}
