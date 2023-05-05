using EPiServer.Core;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Templates.Interfaces;

namespace Perficient.Infrastructure.Templates.Models
{
    [ContentType(
        DisplayName = "Page Templates Folder",
        GUID = "8AAF0B1C-F097-430D-9BB4-4031FDE18EDF",
        AvailableInEditMode = false)]
    [AvailableContentTypes(
        Include = new[]
        {
            typeof(ITemplatePage),
            typeof(TemplatesFolder)
        },
        Exclude = new[]
        {
            typeof(BlockTemplatesFolder),
            typeof(PageTemplatesFolder),
            typeof(TemplatesRootFolder)
        })]
    public class PageTemplatesFolder : ContentFolder
    {
    }
}
