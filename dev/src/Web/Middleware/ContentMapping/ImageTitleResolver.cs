using AutoMapper;
using EPiServer;
using EPiServer.Core;
using Perficient.Web.Features.Media;

namespace Perficient.Web.Middleware.ContentMapping
{
    public class ImageTitleResolver : IMemberValueResolver<object, object, ContentReference, string>
    {
        private readonly IContentRepository _contentRepository;

        public ImageTitleResolver(IContentRepository cotentRepository)
        {
            _contentRepository = cotentRepository;
        }

        public string Resolve(object source, object destination, ContentReference sourceMember, string destMember, ResolutionContext context)
        {
            return sourceMember != null ? _contentRepository.Get<ImageMediaData>(sourceMember)?.AltText : string.Empty;
        }
    }
}

