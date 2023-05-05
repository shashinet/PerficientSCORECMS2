using Boxed.AspNetCore.TagHelpers.OpenGraph;
using Perficient.Web.Middleware.Metadata.OpenGraph.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perficient.Web.Middleware.Metadata.OpenGraph
{
    public class OpenGraphBlogDetailsPage : OpenGraphBasePage
    {
        public OpenGraphBlogDetailsPage(string title, OpenGraphImage image, string url = null) : base(title, image, url)
        {
        }

        public override string Namespace => "website: http://ogp.me/ns/article#";

        public override OpenGraphType Type => OpenGraphType.Article;

        public string Section { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public override void ToString(StringBuilder stringBuilder)
        {
            if (stringBuilder is null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }

            base.ToString(stringBuilder);
            stringBuilder.AppendMetaPropertyContentIfNotNull("article:section", Section);
            stringBuilder.AppendMetaPropertyContentIfNotNull("article:tag", Tags);
        }
    }
}
