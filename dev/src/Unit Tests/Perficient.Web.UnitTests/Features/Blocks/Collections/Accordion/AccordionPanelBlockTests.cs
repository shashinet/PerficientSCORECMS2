using Perficient.Web.Features.Blocks.Collections.Accordion;
using Perficient.Infrastructure.Helpers.FakeHelpers;
using Xunit;

namespace Perficient.Web.UnitTests.Features.Blocks.Collections.Accordion
{
    public class AccordionPanelBlockTests
    {
        [Fact]
        public void SetDefaultValuesTest()
        {
            var newAccordionPanelBlock = FakeBlock<AccordionPanelBlock>.CreateFakeBlock();
            
            newAccordionPanelBlock.CurrentBlock.SetDefaultValues(newAccordionPanelBlock.ContentType);

            Assert.False(newAccordionPanelBlock.CurrentBlock.OpenOnLoad);
            Assert.True(string.IsNullOrWhiteSpace(newAccordionPanelBlock.CurrentBlock.Title));
            Assert.True(string.IsNullOrWhiteSpace(newAccordionPanelBlock.CurrentBlock.ContentHeading));
            Assert.Null(newAccordionPanelBlock.CurrentBlock.RichContent);
            Assert.Null(newAccordionPanelBlock.CurrentBlock.ContentArea);
        }
    }
}