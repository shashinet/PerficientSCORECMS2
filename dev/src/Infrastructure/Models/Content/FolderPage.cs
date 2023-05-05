using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Models.Base;

namespace Perficient.Infrastructure.Models.Content
{
    [ContentType(DisplayName = "Folder Page",
        GUID = "1bc8e78b-40cc-4efc-a561-a0bba89b51ac",
        Description = "A page which allows you to structure pages.",
        GroupName = SystemTabNames.Content)]
    [AvailableContentTypes(IncludeOn = new[] { typeof(BaseStartPage), typeof(FolderPage) })]
    [ImageUrl("~/icons/cms/pages/container.png")]
    public class FolderPage : PageData
    {
    }
}
