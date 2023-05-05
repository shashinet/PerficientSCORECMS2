import React from 'react';

// children
import DesktopHeader from './Desktop';
import MobileHeader from './Mobile';

// hooks
import useWindowSize from '../../hooks/useWindowSize';

// types
import { headerDefaultTypes, headerTypes } from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function HeaderNav(props) {
  const { width } = useWindowSize();
  // eslint-disable-next-line max-len
  const {
    language,
    headerStyle,
    globalStyle,
    logoUrl,
    image,
    tagline,
    navigationItems,
    utilityNavigation,
    skipNavigation,
  } = props;

  return (
    <>
      {width > 1200 ? (
        <DesktopHeader
          language={language}
          headerStyle={headerStyle}
          globalStyle={globalStyle}
          logoUrl={logoUrl}
          image={image}
          tagline={tagline}
          navigationItems={navigationItems}
          utilityNavigation={utilityNavigation}
          skipNavigation={skipNavigation}
        />
      ) : (
        <MobileHeader
          language={language}
          headerStyle={headerStyle}
          globalStyle={globalStyle}
          image={image}
          logoUrl={logoUrl}
          tagline={tagline}
          navigationItems={navigationItems}
          utilityNavigation={utilityNavigation}
        />
      )}
    </>
  );
}

HeaderNav.propTypes = {
  ...headerTypes,
};

HeaderNav.defaultProps = {
  ...headerDefaultTypes,
};
