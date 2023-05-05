using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Templates.Interfaces
{
    public interface ITemplatePage : ITemplateContent
    {
    }

    [UIDescriptorRegistration]
    public class HasTemplatePageDescriptor : UIDescriptor<ITemplatePage> { }
}
