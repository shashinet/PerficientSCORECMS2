using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Interfaces.BlockTypes
{
    public interface INestedContentBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class NestedContentBlockDescriptor : UIDescriptor<INestedContentBlock> { }
}
