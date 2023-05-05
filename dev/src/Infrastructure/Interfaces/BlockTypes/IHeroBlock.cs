using EPiServer.Core;
using EPiServer.Shell;

namespace Perficient.Infrastructure.Interfaces.BlockTypes
{
    public interface IHeroBlock : IContentData
    {
    }

    [UIDescriptorRegistration]
    public class HeroBlockDescriptor : UIDescriptor<IHeroBlock> { }

}
