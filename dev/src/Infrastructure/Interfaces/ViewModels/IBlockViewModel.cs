using EPiServer.Core;

namespace Perficient.Infrastructure.Interfaces.ViewModels
{
    public interface IBlockViewModel<out T> where T : BlockData
    {
        T CurrentBlock { get; }
    }
}
