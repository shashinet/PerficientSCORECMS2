using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Security;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Definitions
{
    [GroupDefinitions]
    public static class TabNames
    {
        [Display(Order = 1)]
        public const string Global = "Global";

        [Display(Order = 10)]
        public const string Default = "Default";

        [Display(Order = 50)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string Header = "Header";

        [Display(Order = 60)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string Footer = "Footer";

        [Display(Name = "Search settings", Order = 65)]
        public const string SearchSettings = "SearchSettings";

        [Display(Order = 70)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string Menu = "Menu";

        [Display(Name = "Site labels", Order = 75)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string SiteLabels = "SiteLabels";

        [Display(Order = 90)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string Tags = "Tags";

        //[Display(Order = 100)]
        //public const string Location = "Location";

        //[Display(Order = 200)]
        //public const string Person = "Person";


        [Display(Order = 250)]
        public const string Teaser = "Teaser";

        [Display(Order = 260)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string MetaData = "Metadata";

        [Display(Order = 300)]        
        public const string Experiment = "Experiment";

        [Display(Name = "Template settings", Order = 350)]
        public const string TemplateSettings = "TemplateSettings";

        [Display(Order = 400)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string Styles = "Styles";

        [Display(Order = 450)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string Css = "Css";

        [Display(Order = 500)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string Scripts = "Scripts";

        [Display(Name = "Settings", Order = 900)]
        public const string Settings = SystemTabNames.Settings;

        [Display(Order = 910)]        
        public const string Search = "Search";

		[Display(Order = 950)]
		public const string FrontOfCard = "Front Of Card";

		[Display(Order = 960)]
		public const string BackOfCard = "Back Of Card";
	}
}
