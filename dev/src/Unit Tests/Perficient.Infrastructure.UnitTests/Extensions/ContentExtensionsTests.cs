using EPiServer.Core;
using Perficient.Infrastructure.Extensions;
using Perficient.Infrastructure.Helpers.FakeHelpers;
using Xunit;

namespace Perficient.Infrastructure.UnitTests.Extensions
{
    public class ContentExtensionsTests
    {
        [Fact]
        public void ContentLink_SharedBlock()
        {
            // FakeBlock helper adds the IContent proxy class
            var mockedBlock = FakeBlock<BlockData>.CreateFakeBlock();
            var testReference = new ContentReference(9999999);

            mockedBlock.CurrentBlockAsIContent.ContentLink = testReference;

            var referenceFromExtensionMethod = mockedBlock.CurrentBlock.ContentLink();

            Assert.Equal(testReference, referenceFromExtensionMethod);
        }

        [Fact]
        public void ContentLink_LocalBlock()
        {
            var mockedBlock = new BlockData();

            var reference = mockedBlock.ContentLink();

            Assert.True(ContentReference.IsNullOrEmpty(reference));
        }

        [Fact]
        public void TryGetBlockContent_SharedBlock()
        {
            // FakeBlock helper adds the IContent proxy class
            var mockedBlock = FakeBlock<BlockData>.CreateFakeBlock();

            var success = mockedBlock.CurrentBlock.TryGetBlockContent(out IContent blockContent);

            Assert.True(success);
            Assert.True(blockContent is not null);
        }

        [Fact]
        public void TryGetBlockContent_LocalBlock()
        {
            var mockedBlock = new BlockData();

            var success = mockedBlock.TryGetBlockContent(out IContent blockContent);

            Assert.False(success);
            Assert.True(blockContent is null);
        }
    }
}
