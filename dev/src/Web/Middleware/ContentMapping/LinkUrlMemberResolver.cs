using AutoMapper;
using EPiServer;
using EPiServer.Web.Internal;
using EPiServer.Web.Routing;

namespace Perficient.Web.Middleware.ContentMapping
{
    public class LinkUrlMemberResolver : IMemberValueResolver<object, object, Url, string>
    {
        private readonly UrlResolver _urlResolver;

        public LinkUrlMemberResolver(UrlResolver urlResolver)
        {
            _urlResolver = urlResolver;
        }

        public string Resolve(object source, object destination, Url sourceMember, string destMember, ResolutionContext context)
        {
            if (sourceMember == null || sourceMember.IsEmpty())
            {
                return string.Empty;
            }

            if (sourceMember.IsAbsoluteUri)
            {
                return sourceMember.ToString();
            }

            return UrlEncoder.Encode(_urlResolver.GetUrl(sourceMember.ToString()) ?? sourceMember.ToString());
        }
    }
}
