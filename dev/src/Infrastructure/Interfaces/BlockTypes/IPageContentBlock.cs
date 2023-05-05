using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Interfaces.BlockTypes

{
    public interface IPageContentBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class PageContentBlockDescriptor : UIDescriptor<IPageContentBlock> { }
}
