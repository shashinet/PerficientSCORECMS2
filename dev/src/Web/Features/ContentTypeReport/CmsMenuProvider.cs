using EPiServer.Shell.Navigation;
using System.Collections.Generic;


namespace Perficient.Web.Features.ContentTypeReport
{
    [MenuProvider]
    public class CmsMenuProvider : IMenuProvider
    {
        IEnumerable<MenuItem> IMenuProvider.GetMenuItems()
        {
            var menuItems = new List<MenuItem>
            {
                new UrlMenuItem("Inventory Report", "/global/cms/ContentTypeReport", "/Admin/LegacyContentTypeReport")
                {
                    IsAvailable = request => true,
                    SortIndex = 100
                },
                new UrlMenuItem(string.Empty, "/global/cms/ContentTypeReport/details", "/Admin/LegacyContentTypeReport/ContentDetailsReport")
                {
                    IsAvailable = (_) => false,
                    SortIndex = 101,
                    

                },
                new UrlMenuItem(string.Empty, "/global/cms/ContentTypeReport/references", "/Admin/LegacyContentTypeReport/ContentReferencesReport")
                {
                    IsAvailable = (_) => false,
                    SortIndex = 102
                }
            };

            return menuItems;
        }
    }
}
