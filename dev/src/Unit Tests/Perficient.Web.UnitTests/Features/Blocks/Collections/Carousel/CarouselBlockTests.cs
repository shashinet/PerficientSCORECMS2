using Perficient.Web.Features.Blocks.Collections.Carousel;
using Perficient.Infrastructure.Helpers.FakeHelpers;
using Xunit;

namespace Perficient.Web.UnitTests.Features.Blocks.Collections.Carousel
{
    public class CarouselBlockTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("test", " test")]
        [InlineData("test1,test2", " test1 test2")]
        public void GetClassListTest(string input, string expectedResult)
        {
            var carouselBlock = FakeBlock<CarouselBlock>.CreateFakeBlock();
            carouselBlock.CurrentBlock.CarouselStyle = input;

            var classList = carouselBlock.CurrentBlock.GetClassList();

            Assert.Equal(expectedResult, classList);
        }
        
        [Fact]
        public void SetDefaultValuesTest()
        {
            var carouselBlock = FakeBlock<CarouselBlock>.CreateFakeBlock();

            carouselBlock.CurrentBlock.SetDefaultValues(carouselBlock.ContentType);
            
            Assert.Equal("default", carouselBlock.CurrentBlock.CarouselStyle);
            Assert.Null(carouselBlock.CurrentBlock.CarouselPanes);
        }
    }
}