import React from 'react';

// children
import SecondaryNavigationDesktop from './Desktop';
import SecondaryNavigationMobile from './Mobile';

// hooks
import useWindowSize from '../../hooks/useWindowSize';

// types
import { secondaryNavDefaultTypes, secondaryNavTypes } from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function SecondaryNavigation(props) {
  const { title, secondaryNavStyle, navigationItems, infoPanel, buttonText, buttonStyle } = props;

  const { width } = useWindowSize();
  return (
    <>
      {width > 1199 ? (
        <>
          <SecondaryNavigationDesktop
            secondaryNavStyle={secondaryNavStyle}
            title={title}
            navigationItems={navigationItems}
            infoPanel={infoPanel}
            buttonText={buttonText}
            buttonStyle={buttonStyle}
          />
        </>
      ) : (
        <>
          <SecondaryNavigationMobile
            secondaryNavStyle={secondaryNavStyle}
            title={title}
            navigationItems={navigationItems}
            buttonText={buttonText}
            buttonStyle={buttonStyle}
          />
        </>
      )}
    </>
  );
}

SecondaryNavigation.propTypes = {
  ...secondaryNavTypes,
}.isRequired;

SecondaryNavigation.defaultProps = {
  ...secondaryNavDefaultTypes,
};
