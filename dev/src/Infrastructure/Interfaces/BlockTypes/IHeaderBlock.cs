using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Interfaces.BlockTypes
{
    public interface IHeaderBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class HeaderBlockDescriptor : UIDescriptor<IHeaderBlock> { }
}
