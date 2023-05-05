using EPiServer.Shell.ObjectEditing;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Perficient.Infrastructure.Attributes;
using System;
using System.Linq;

namespace Perficient.Web.Features.Blocks.Fields.Heading
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HeadingStylesAttribute : Attribute, IDisplayMetadataProvider
    {
        public string[] HeadingStyles { get; set; }

        public HeadingStylesAttribute(params string[] headingStyles)
        {
            HeadingStyles = headingStyles;
        }

        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context.DisplayMetadata.AdditionalValues[ExtendedMetadata.ExtendedMetadataDisplayKey] is not ExtendedMetadata extendedMetadata)
            {
                return;
            }

            if (extendedMetadata.ContainerType != typeof(HeadingBlock))
            {
                return;
            }


            var headingProperty = extendedMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(HeadingBlock.HeadingStyle));
            if (headingProperty == null)
            {
                return;
            }

            if (headingProperty?.Attributes?.FirstOrDefault(a => a.GetType() == typeof(QuickSelectAttribute)) is QuickSelectAttribute quickSelect)
            {
                quickSelect.QuickListItems = this.HeadingStyles;
            }
        }
    }
}
