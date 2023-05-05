using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Perficient.Web.Features.Articles.Models;

namespace Perficient.Web.Features.Articles.Extensions
{
    public static class ArticleCategoryExtensions
    {
        private static readonly Injected<IContentRepository> _contentRepository = default;
        public static ArticleCategory GetArticleCategory(this ContentReference contentRefernce)
        {
            if (contentRefernce == null)
            {
                return null;
            }
            
            return _contentRepository.Service.Get<ArticleCategory>(contentRefernce);
        }
    }
}

