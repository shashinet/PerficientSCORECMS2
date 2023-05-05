using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Interfaces.BlockTypes
{
    public interface IAlertBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class AlertBlockDescriptor : UIDescriptor<IAlertBlock> { }
}
