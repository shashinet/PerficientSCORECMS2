using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Templates.Interfaces
{
    public interface ITemplateContent : IContentData
    {
        ContentReference SelectedTemplate { get; set; }
        ContentReference OldTemplate { get; set; }
    }
}
