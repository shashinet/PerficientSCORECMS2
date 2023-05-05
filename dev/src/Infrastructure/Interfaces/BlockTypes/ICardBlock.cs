using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Interfaces.BlockTypes
{
    public interface ICardBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class CardBlockDescriptor : UIDescriptor<ICardBlock> { }
}
