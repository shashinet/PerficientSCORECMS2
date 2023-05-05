using EPiServer.Core;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Templates.Interfaces;

namespace Perficient.Infrastructure.Templates.Models
{
    [ContentType(
        DisplayName = "Templates Folder",
        GUID = "5CBEB0FC-3282-426E-99DF-417EF49AF831")]
    [AvailableContentTypes(
        Include = new[]
        {
            typeof(ITemplateBlock),
            typeof(ITemplatePage),
            typeof(TemplatesFolder)
        },
        Exclude = new[]
        {
            typeof(PageTemplatesFolder),
            typeof(BlockTemplatesFolder),
            typeof(TemplatesRootFolder)
        })]
    public class TemplatesFolder : ContentFolder { }
}
