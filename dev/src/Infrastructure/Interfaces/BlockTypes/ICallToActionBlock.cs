using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Interfaces.BlockTypes
{
    public interface ICallToActionBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class CallToActionBlockDescriptor : UIDescriptor<ICallToActionBlock> { }
}
