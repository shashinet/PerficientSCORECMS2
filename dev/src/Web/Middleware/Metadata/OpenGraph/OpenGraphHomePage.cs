using Boxed.AspNetCore.TagHelpers.OpenGraph;

namespace Perficient.Web.Middleware.Metadata.OpenGraph
{
    public class OpenGraphHomePage : OpenGraphBasePage
    {
        public OpenGraphHomePage(string title, OpenGraphImage image, string url = null) : base(title, image, url)
        {
        }

        public override string Namespace => "website: http://ogp.me/ns/website#";

        public override OpenGraphType Type => OpenGraphType.Website;
    }
}
