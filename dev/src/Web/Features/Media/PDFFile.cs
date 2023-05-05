using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.Media
{
    [ContentType(GUID = "58341C80-E78F-4F83-AF11-3B48563B41CA")]
    [MediaDescriptor(ExtensionString = "pdf")]
    public class PdfFile : MediaData
    {
        [Editable(false)]
        public virtual string FileSize { get; set; }
    }

}
