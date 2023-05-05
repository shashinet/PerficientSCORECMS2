// styles
import './styles/index.scss';

// vendor
import 'what-input';

import optiReactHarness from '../../core/js/OptiReactHarness';

// React components
import FooterNavigation from '../../core/react/Footer';
import HeaderNavigation from '../../core/react/Header';
import VerticalTabset from '../../core/react/TabSet/VerticalTabset';
import LargeButtonTabset from '../../core/react/TabSet/LargeButtonTabset';
import SecondaryNavigation from '../../core/react/SecondaryNavigation';

// non-React Components
import Accordion from '../../core/js/Accordion';
import AlertBanners from '../../core/js/AlertBanners';
import ScoreVideoLoader from '../../core/js/Video';
import Tabset from '../../core/js/Tabset';
import TabNavButtons from '../../core/js/Tabset/TabWithNavigationButtons';

// attach React components React globally
optiReactHarness({
  // add components here
  FooterNavigation,
  HeaderNavigation,
  VerticalTabset,
  LargeButtonTabset,
  SecondaryNavigation,
});

// init non-React components globally
Accordion.init();
AlertBanners.init();
ScoreVideoLoader.init();
Tabset.init();
TabNavButtons.init();
