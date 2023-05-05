using EPiServer.Core;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Html;
using Perficient.Infrastructure.Interfaces.Content;
using Perficient.Infrastructure.Interfaces.ViewModels;

namespace Perficient.Infrastructure.Models.ViewModels
{
    public class ContentViewModel<TContent> : IContentViewModel<TContent> where TContent : IContent
    {
        public ContentViewModel() : this(default)
        {
        }

        public ContentViewModel(TContent currentContent)
        {
            CurrentContent = currentContent;
        }

        public TContent CurrentContent { get; set; }


        public HtmlString SchemaMarkup
        {
            get
            {
                //See if there's a schema data mapper for this content type and, if so, generate some schema markup
                if (ServiceLocator.Current.TryGetExistingInstance(out ISchemaDataMapper<TContent> mapper))
                {
                    return new HtmlString($"<script type=\"application/ld+json\">{mapper.Map(CurrentContent).ToHtmlEscapedString()}</script>");
                }
                return new HtmlString(string.Empty);
            }
        }
    }

    public static class ContentViewModel
    {
        public static ContentViewModel<T> Create<T>(T content) where T : IContent => new ContentViewModel<T>(content);
    }
}
