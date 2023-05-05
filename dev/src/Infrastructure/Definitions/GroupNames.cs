using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Definitions
{
    [GroupDefinitions]
    public static class GroupNames
    {
        [Display(Name = "Content", Order = 400)]
        public const string Content = "Content";

        [Display(Order = 450)]
        public const string Articles = "Articles";

        [Display(Name = "Buttons", Order = 500)]
        public const string Buttons = "Buttons";

        [Display(Order = 520)]
        public const string Layout = "Layout";

        [Display(Order = 520)]
        public const string Collections = "Collections";

        [Display(Order = 550)]
        public const string Videos = "Videos";

        [Display(Order = 570)]
        public const string Forms = "Forms";

        [Display(Order = 580)]
        public const string Multimedia = "Multimedia";

        [Display(Order = 600)]
        public const string SocialMedia = "Social media";

        [Display(Order = 610)]
        public const string Social = "Social";

        [Display(Order = 620)]
        public const string Syndication = "Syndication";

        [Display(Order = 630)]
        public const string Navigation = "Navigation";

        [Display(Order = 670)]
        public const string Experimentation = "Experimentation";

        [Display(Order = 710)]
        public const string Specialized = "Specialized";

        [Display(Order = 810)]
        public const string ContentDelivery = "Content Delivery";

        [Display(Order = 820)]
        public const string Templates = "Templates";

    }
}
