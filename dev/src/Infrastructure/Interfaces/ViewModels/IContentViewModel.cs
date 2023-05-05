using EPiServer.Core;
using Microsoft.AspNetCore.Html;

namespace Perficient.Infrastructure.Interfaces.ViewModels
{
    public interface IContentViewModel<out TContent> where TContent : IContent
    {
        TContent CurrentContent { get; }
        HtmlString SchemaMarkup { get; }
    }
}
