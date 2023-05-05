using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Templates.Interfaces
{
    public interface ITemplateBlock : ITemplateContent
    {
    }

    [UIDescriptorRegistration]
    public class HasTemplateBlockDescriptor : UIDescriptor<ITemplateBlock> { }
}
