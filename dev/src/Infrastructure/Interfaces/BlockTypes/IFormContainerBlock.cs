using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Interfaces.BlockTypes
{
    public interface IFormContainerBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class FormContainerDescriptor : UIDescriptor<IFormContainerBlock> { }
}
