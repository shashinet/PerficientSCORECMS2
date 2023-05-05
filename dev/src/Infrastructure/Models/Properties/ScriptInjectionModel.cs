using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Newtonsoft.Json;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.Models.Content;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Models.Properties
{
    public class ScriptInjectionModel
    {
        [Required]
        [CultureSpecific]
        [AllowedTypes(typeof(BasePage), typeof(FolderPage))]
        [Display(Name = "Root (Scripts will inject for this page and all children pages)", Description = "Scripts will inject for this page and all children pages", Order = 10)]
        public virtual ContentReference ScriptRoot { get; set; }

        [AllowedTypes(typeof(CodingFile))]
        [Display(Name = "Script files", Order = 20)]
        public virtual IList<ContentReference> ScriptFiles { get; set; }

        [UIHint(UIHint.Textarea)]
        [Display(Name = "External Scripts", Order = 30)]
        public virtual string ExternalScripts { get; set; }

        [UIHint(UIHint.Textarea)]
        [Display(Name = "Inline Scripts", Order = 40)]
        public virtual string InlineScripts { get; set; }
    }
}
