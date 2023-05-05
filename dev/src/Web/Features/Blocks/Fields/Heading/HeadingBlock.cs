using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Blocks.Fields.Heading
{
    [ContentType(DisplayName = "Customized Heading", GUID = "{CCB22F4F-9821-4847-A5C0-A3C8F27A3982}",
        Description = "A block that allows you to specify the Heading Element type and text.", AvailableInEditMode = false)]
    public class HeadingBlock : BlockData
    {
        [Display(Name = "Heading Text", Description = "Text to display as heading element between H tags.", Order = 10)]
        [CultureSpecific]
        [Searchable]
        public virtual string Text { get; set; }

        [QuickSelect("H1", "H2", "H3")]
        [Display(Name = "Heading Style", Description = "The heading level.", Order = 10)]
        [CultureSpecific]
        public virtual string HeadingStyle { get; set; }
    }
}
