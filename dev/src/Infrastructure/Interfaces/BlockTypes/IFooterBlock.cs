using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Interfaces.BlockTypes
{
    public interface IFooterBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class FooterBlockDescriptor : UIDescriptor<IFooterBlock> { }
}
