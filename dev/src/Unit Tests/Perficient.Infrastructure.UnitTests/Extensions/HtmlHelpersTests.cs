//using FakeItEasy;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Perficient.Infrastructure.Models.Properties;
//using Perficient.Infrastructure.Settings.Interfaces;
//using Perficient.Infrastructure.Settings.Models.Content;
//using System.Collections.Generic;
//using Xunit;

//namespace Perficient.Infrastructure.UnitTests.Extensions
//{
//    public class HtmlHelpersTests
//    {
//        [Fact]
//        public void GetPieColorClass_ColorsExist()
//        {
//            var mockedHtmlHelper = A.Fake<IHtmlHelper>();
//            var mockedSettingsService = A.Fake<ISettingsService>();

//            var mockedStyleSettings = new StyleSettings
//            {
//                Colors = new List<ScoreColor>()
//                {
//                    new ScoreColor { Name = "Black", ColorCode = "#000000" },
//                    new ScoreColor { Name = "Text Black", ColorCode = "#2A3544" },
//                    new ScoreColor { Name = "Dark Violet", ColorCode = "#180945" },
//                    new ScoreColor { Name = "Violet", ColorCode = "#5627E7" },
//                    new ScoreColor { Name = "White", ColorCode = "#FFFFFF" },
//                    new ScoreColor { Name = "Evergreen", ColorCode = "#005738" },
//                    new ScoreColor { Name = "Sun Yellow", ColorCode = "#FFE805" },
//                    new ScoreColor { Name = "Sky Blue", ColorCode = "#38A8FF" },
//                    new ScoreColor { Name = "Light Gray", ColorCode = "#F5F7FB" },
//                    new ScoreColor { Name = "Gray", ColorCode = "#D2D2D2" },
//                    new ScoreColor { Name = "Dark Gray", ColorCode = "#6E6E6E" },
//                }
//            };

//            A.CallTo(() => mockedSettingsService.GetSiteSettings<StyleSettings>(default)).Returns(mockedStyleSettings);

//            //Single word name
//            Assert.Equal("pie-black", mockedHtmlHelper.GetPieColorClass(mockedSettingsService, "#000000"));

//            // Multi-word name
//            Assert.Equal("pie-sun-yellow", mockedHtmlHelper.GetPieColorClass(mockedSettingsService, "#FFE805"));
//        }

//        [Fact]
//        public void GetPieColorClass_ColorsNull()
//        {
//            var mockedHtmlHelper = A.Fake<IHtmlHelper>();
//            var mockedSettingsService = A.Fake<ISettingsService>();

//            var mockedStyleSettings = new StyleSettings();

//            A.CallTo(() => mockedSettingsService.GetSiteSettings<StyleSettings>(default)).Returns(mockedStyleSettings);

//            //Single word name
//            Assert.Equal(string.Empty, mockedHtmlHelper.GetPieColorClass(mockedSettingsService, "#000000"));

//            // Multi-word name
//            Assert.Equal(string.Empty, mockedHtmlHelper.GetPieColorClass(mockedSettingsService, "#FFE805"));
//        }

//        [Fact]
//        public void GetPieColorClass_ColorsNotFound()
//        {
//            var mockedHtmlHelper = A.Fake<IHtmlHelper>();
//            var mockedSettingsService = A.Fake<ISettingsService>();

//            var mockedStyleSettings = new StyleSettings
//            {
//                Colors = new List<ScoreColor>()
//                {
//                    new ScoreColor { Name = "Black", ColorCode = "#000000" },
//                    new ScoreColor { Name = "Text Black", ColorCode = "#2A3544" },
//                    new ScoreColor { Name = "Dark Violet", ColorCode = "#180945" },
//                    new ScoreColor { Name = "Violet", ColorCode = "#5627E7" },
//                    new ScoreColor { Name = "White", ColorCode = "#FFFFFF" },
//                    new ScoreColor { Name = "Evergreen", ColorCode = "#005738" },
//                    new ScoreColor { Name = "Sun Yellow", ColorCode = "#FFE805" },
//                    new ScoreColor { Name = "Sky Blue", ColorCode = "#38A8FF" },
//                    new ScoreColor { Name = "Light Gray", ColorCode = "#F5F7FB" },
//                    new ScoreColor { Name = "Gray", ColorCode = "#D2D2D2" },
//                    new ScoreColor { Name = "Dark Gray", ColorCode = "#6E6E6E" },
//                }
//            };

//            A.CallTo(() => mockedSettingsService.GetSiteSettings<StyleSettings>(default)).Returns(mockedStyleSettings);

//            Assert.Equal(string.Empty, mockedHtmlHelper.GetPieColorClass(mockedSettingsService, "#000001"));
//        }
//    }
//}
