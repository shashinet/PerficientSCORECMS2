import React from 'react';

// Components
import SearchButtonNav from '../Header/Search';
import MegaMenuFlyout from '../Header/MenuItems/megaMenuFlyout';
import NavigationLink from '../Header/MenuItems/navigationLink';
import LanguageSelector from '../Header/MenuItems/languageSelector';
import MenuList from '../Footer/MenuList/menuList';
import InfoCard from '../SecondaryNavigation/InfoCard';
import CtaCard from '../SecondaryNavigation/CtaCard';
// eslint-disable-next-line import/no-cycle
import NavigationPanel from '../Footer/Panels/navigationPanel';
import Button from '../Button';
import Card from '../Card';
import SectionHeader from '../SectionHeader';
import SectionHero from '../SectionHero';
import Tile from '../Tile';
import Video from '../Video/video';

// Component Mapping Registration
const Components = {
  MegaMenuFlyout,
  NavigationLink,
  SearchButton: SearchButtonNav,
  MenuList,
  NavigationPanel,
  LanguageSelector,
  SecondaryNavigationInfoItem: InfoCard,
  SecondaryNavigationCtaItem: CtaCard,
  Button,
  Card,
  SectionHeader,
  SectionHero,
  Tile,
  Video,
};

export default (block) => {
  const {
    id,
    contentType,
  } = block;
  if (typeof contentType !== 'undefined') {
    return React.createElement(Components[contentType], { block });
  }
  return React.createElement(
    () => <div>{contentType}</div>, {
      key: id,
    },
  );
};
