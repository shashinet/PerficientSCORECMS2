using Perficient.Infrastructure.Models.Properties;
using System.Collections.Generic;

namespace Perficient.Infrastructure.Settings.Models
{
    public class JsonFileStyleSettingsModel
    {
        public IList<ScoreColor> Colors { get; set; }
        public IList<ScoreColor> FontColors { get; set; }
        public IList<ScoreButtonStyle> ButtonStyles { get; set; }
        public IList<ScoreClass> ImageClasses { get; set; }
        public IList<ScoreClass> CalloutClasses { get; set; }
        public IList<ScoreClass> CarouselClasses { get; set; }
        public IList<ScoreClass> CarouselPaneClasses { get; set; }
        public IList<ScoreClass> DocumentHeaderClasses { get; set; }
        public IList<ScoreClass> PageLayoutClasses { get; set; }
        public IList<ScoreClass> CardClasses { get; set; }
        public IList<ScoreClass> PricingCardClasses { get; set; }
        public IList<ScoreClass> TileClasses { get; set; }
        public IList<ScoreClass> StyleBoxClasses { get; set; }
        public IList<ScoreClass> HorizontalRuleClasses { get; set; }
        public IList<ScoreClass> SectionHeroClasses { get; set; }
        public IList<ScoreClass> StripeClasses { get; set; }
        public IList<ScoreClass> FooterClasses { get; set; }
        public IList<ScoreClass> HeaderClasses { get; set; }
        public IList<ScoreClass> SideNavClasses { get; set; }
        public IList<ScoreClass> StyleClasses { get; set; }
        public IList<ScoreButtonSize> ButtonSizes { get; set; }
    }
}
