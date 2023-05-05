using AutoMapper;
using EPiServer.Core;

namespace Perficient.Web.Middleware.ContentMapping
{
    public class XhtmlStringMemberResolver : IMemberValueResolver<object, object, XhtmlString, string>
    {

        public string Resolve(object source, object destination, XhtmlString sourceMember, string destMember, ResolutionContext context)
        {
            if (sourceMember == null || sourceMember.IsEmpty)
            {
                return string.Empty;
            }

            return sourceMember.ToHtmlString() ?? sourceMember.ToString();
        }
    }
}
