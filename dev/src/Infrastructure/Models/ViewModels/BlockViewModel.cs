using EPiServer.Core;
using Perficient.Infrastructure.Interfaces.ViewModels;

namespace Perficient.Infrastructure.Models.ViewModels
{
    public class BlockViewModel<T> : IBlockViewModel<T> where T : BlockData
    {
        public BlockViewModel(T currentBlock) => CurrentBlock = currentBlock;

        public T CurrentBlock { get; }
    }
}
