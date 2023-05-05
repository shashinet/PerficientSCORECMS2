using EPiServer;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Navigation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Navigation.Controllers
{
    public class SectionMenuSpiderBlockComponent : AsyncPartialContentComponent<SectionMenuSpiderBlock>
    {
        private readonly IContentRepository _contentRepository;

        public SectionMenuSpiderBlockComponent(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(SectionMenuSpiderBlock currentContent)
        {
            if (currentContent.ParentPage != null)
            {
                var rootPage = _contentRepository.Get<BasePage>(currentContent.ParentPage);

                currentContent.SpiderData = GetChildPages(rootPage);
            }
            return await Task.FromResult(View("~/Features/Navigation/Views/SectionMenuSpiderBlock.cshtml", currentContent));
        }


        private List<SectionMenuSpiderData> GetChildPages(BasePage content)
        {
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();

            var sectionMenuSpiderData = new List<SectionMenuSpiderData>();

            var childPages = _contentRepository.GetChildren<BasePage>(content.ContentLink);

            foreach (var page in childPages)
            {
                if (page.VisibleInMenu)
                {
                    sectionMenuSpiderData.Add(new SectionMenuSpiderData
                    {
                        Id = page.ContentLink.ID,
                        Name = string.IsNullOrWhiteSpace(page.NavigationTitle) ? page.Name : page.NavigationTitle,
                        Url = urlResolver.GetUrl(page),
                        Children = GetChildPages(page)
                    });
                }
            }

            return sectionMenuSpiderData;
        }
    }
}
