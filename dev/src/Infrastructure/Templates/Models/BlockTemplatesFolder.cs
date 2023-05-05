using EPiServer.Core;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Templates.Interfaces;

namespace Perficient.Infrastructure.Templates.Models
{
    [ContentType(
        DisplayName = "Block Templates Folder",
        GUID = "4A3CAE2F-0F7A-4810-B50F-991F08C4CFFD",
        AvailableInEditMode = false)]
    [AvailableContentTypes(
        Include = new[]
        {
            typeof(ITemplateBlock),
            typeof(TemplatesFolder)
        },
        Exclude = new[]
        {
            typeof(PageTemplatesFolder),
            typeof(BlockTemplatesFolder),
            typeof(TemplatesRootFolder)
        })]
    public class BlockTemplatesFolder : ContentFolder { }
}
