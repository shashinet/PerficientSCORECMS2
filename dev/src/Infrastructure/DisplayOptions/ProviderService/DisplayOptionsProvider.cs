using Perficient.Infrastructure.DisplayOptions.Interface;
using Perficient.Infrastructure.DisplayOptions.Models;
using System.Collections.Generic;
using static Perficient.Infrastructure.DisplayOptions.Constants.DisplayOptionConstants;

namespace Perficient.Infrastructure.DisplayOptions.ProviderService
{
    public class DisplayOptionsProvider : IDisplayOptionsProvider
    {
        public IEnumerable<DisplayOption> GetList()
        {
            return new List<DisplayOption>()
            {
                new DisplayOption
                {
                    Name = DisplayOptionNames.Full,
                    RenderingTag = ContentAreaTags.FullWidth,
                    IconClass = "epi-icon__layout--full",
                    Order = 1
                },
                new DisplayOption
                {
                    Name = DisplayOptionNames.ThreeFourth,
                    RenderingTag = ContentAreaTags.ThreeFourthsWidth,
                    IconClass = "epi-icon__layout--three-fourths",
                    Order = 2
                },
                new DisplayOption
                {
                    Name = DisplayOptionNames.TwoThirds,
                    RenderingTag = ContentAreaTags.TwoThirdsWidth,
                    IconClass = "epi-icon__layout--two-thirds",
                    Order = 2
                },
                new DisplayOption
                {
                    Name = DisplayOptionNames.Half,
                    RenderingTag = ContentAreaTags.HalfWidth,
                    IconClass = "epi-icon__layout--half",
                    Order = 2
                },
                new DisplayOption
                {
                    Name = DisplayOptionNames.OneThird,
                    RenderingTag = ContentAreaTags.OneThirdWidth,
                    IconClass = "epi-icon__layout--one-third",
                    Order = 1
                },
                new DisplayOption
                {
                    Name = DisplayOptionNames.OneFourth,
                    RenderingTag = ContentAreaTags.OneFourthWidth,
                    IconClass = "epi-icon__layout--one-fourth",
                    Order = 1
                },
                new DisplayOption
                {
                    Name = DisplayOptionNames.EdgeToEdge,
                    RenderingTag = ContentAreaTags.EdgeToEdge,
                    IconClass = "epi-icon__layout--edge-to-edge",
                    Order = 1
                },
                new DisplayOption
                {
                    Name = DisplayOptionNames.Contained,
                    RenderingTag = ContentAreaTags.Contained,
                    IconClass = "epi-icon__layout--contained",
                    Order = 1
                },
                new DisplayOption
                {
                    Name = DisplayOptionNames.Fixed,
                    RenderingTag = ContentAreaTags.Fixed,
                    IconClass = "epi-icon__layout--fixed",
                    Order = 1
                },
                new DisplayOption
                {
                    Name = DisplayOptionNames.Offset,
                    RenderingTag = ContentAreaTags.Offset,
                    IconClass = "epi-icon__layout--offset",
                    Order = 1
                }
            };
        }
    }
}
