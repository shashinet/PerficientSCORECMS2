using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Models.Base;

namespace Perficient.Web.Features.Pages.Home
{
    [ContentType(DisplayName = "Home Page",
        GUID = "452d1812-7385-42c3-8073-c1b7481e7b20",
        Description = "Used for home page of all sites",
        AvailableInEditMode = true,
        GroupName = GroupNames.Content)]
    [ImageUrl("/icons/cms/pages/CMS-icon-page-02.png")]
    public class HomePage : BaseStartPage
    {

    }
}
