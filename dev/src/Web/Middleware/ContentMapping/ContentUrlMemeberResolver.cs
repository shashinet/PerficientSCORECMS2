using AutoMapper;
using EPiServer.Core;
using EPiServer.Web.Routing;

namespace Perficient.Web.Middleware.ContentMapping
{
    public class ContentUrlMemeberResolver : IMemberValueResolver<object, object, ContentReference, string>
    {
        private readonly UrlResolver _urlResolver;

        public ContentUrlMemeberResolver(UrlResolver urlResolver)
        {
            _urlResolver = urlResolver;
        }

        public string Resolve(object source, object destination, ContentReference sourceMember, string destMember, ResolutionContext context)
        {
            return _urlResolver.GetUrl(sourceMember);
        }
    }
}

