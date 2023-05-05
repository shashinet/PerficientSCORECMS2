using Perficient.Web.Features.Blocks.Collections.Carousel;
using Perficient.Infrastructure.Helpers.FakeHelpers;
using Xunit;

namespace Perficient.Web.UnitTests.Features.Blocks.Collections.Carousel
{
    public class CarouselPanelBlockTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("test", " test")]
        [InlineData("test1,test2", " test1 test2")]
        public void GetClassListTest(string input, string expectedResult)
        {
            //arrange
            var carouselPanelBlock = FakeBlock<CarouselPaneBlock>.CreateFakeBlock();
            carouselPanelBlock.CurrentBlock.CarouselPaneStyle = input;

            //act
            var classList = carouselPanelBlock.CurrentBlock.GetClassList();

            //assert
            Assert.Equal(expectedResult, classList);
        }
        
        [Fact]
        public void SetDefaultValuesTest()
        {
            //arrange
            var newCarouselPanelBlock = FakeBlock<CarouselPaneBlock>.CreateFakeBlock();


            //act
            newCarouselPanelBlock.CurrentBlock.SetDefaultValues(newCarouselPanelBlock.ContentType);

            //assert
            Assert.Equal("default", newCarouselPanelBlock.CurrentBlock.CarouselPaneStyle);
            Assert.Null(newCarouselPanelBlock.CurrentBlock.ContentArea);
            Assert.Null(newCarouselPanelBlock.CurrentBlock.BackgroundImage);
            Assert.True(string.IsNullOrWhiteSpace(newCarouselPanelBlock.CurrentBlock.BackgroundColor));
            Assert.True(string.IsNullOrWhiteSpace(newCarouselPanelBlock.CurrentBlock.TextColor));
        }
    }
}