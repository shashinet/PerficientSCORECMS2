using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Perficient.Web.Features.Media
{
    [ContentType(GUID = "20644be7-3ca1-4f84-b893-ee021b73ce6c", DisplayName = "Image Media")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png,webp")]
    public class ImageMediaData : ImageData
    {
        [Editable(false)]
        [ImageDescriptor(Width = 256, Height = 256)]
        public virtual Blob LargeThumbnail { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Alt text",
            Description = "Alternate text for while image is loading and for the Aria accessability labels.",
            GroupName = SystemTabNames.Content,
            Order = 50)]

        public virtual string AltText { get; set; }

        [Searchable]
        public virtual string Caption { get; set; }
        [Searchable]
        public virtual IList<string> Tags { get; set; }

        public string GetFriendlyAltText()
        {
            if (!string.IsNullOrWhiteSpace(AltText))
                return AltText;

            try
            {
                var retVal = System.IO.Path.GetFileNameWithoutExtension(Name);
                return Regex.Replace(retVal, @"[^a-zA-Z0-9]+", " ");
            }
            catch
            {
                return AltText;
            }
        }
    }
}





