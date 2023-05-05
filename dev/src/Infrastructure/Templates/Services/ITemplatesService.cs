using EPiServer.Core;

namespace Perficient.Infrastructure.Templates.Services
{
    public interface ITemplatesService
    {
        ContentReference TemplatesRoot { get; set; }
        void InitializeTemplates();
        void UninitializeTemplates();
        void UpdateTemplates();

        int SaveAsTemplate(int sourceContentId, int sourceTypeId, int folderId, out string message);
    }
}
