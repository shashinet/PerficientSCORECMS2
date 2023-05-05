import React from 'react';

// children
import NavigationMenuItem from '../MenuItem';

// styles
import styles from './index.module.scss';

// types
import { secondaryNavTypes } from '../types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function SecondaryNavigationMobile(props) {
  const { title, secondaryNavStyle, navigationItems } = props;

  const [isComponentVisible, setIsComponentVisible] = React.useState(false);

  const handleOpenNavigation = () => {
    setIsComponentVisible(!isComponentVisible);
    document.querySelector('body').classList.toggle('position-fixed');
  };

  return (
    <div className={[...secondaryNavStyle, styles.secondaryNavigationWrapper].join(' ')}>
      <div
        aria-label="secondary-navigation"
        className={[
          `${isComponentVisible ? 'mega-menu secondary-navigation open' : 'mega-menu secondary-navigation'}`,
        ].join(' ')}
      >
        <div className="header-area">
          <h5>{title}</h5>
          <span
            role="menuitem"
            className="icon-button"
            onClick={handleOpenNavigation}
            onKeyDown={handleOpenNavigation}
            tabIndex={0}
          >
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M2.25173 10.5493C1.42331 10.5493 0.751734 11.2208 0.751734 12.0493C0.751735 12.8777 1.42331 13.5493 2.25173 13.5493L10.4666 13.5493L10.4666 21.7646C10.4666 22.593 11.1382 23.2646 11.9666 23.2646C12.795 23.2646 13.4666 22.593 13.4666 21.7646L13.4666 13.5493L21.8791 13.5493C22.7076 13.5493 23.3791 12.8777 23.3791 12.0493C23.3791 11.2208 22.7076 10.5493 21.8791 10.5493L13.4666 10.5493L13.4666 2.13716C13.4666 1.30874 12.795 0.637164 11.9666 0.637164C11.1382 0.637164 10.4666 1.30874 10.4666 2.13716L10.4666 10.5493L2.25173 10.5493Z"
                fill="#222222"
              />
            </svg>
          </span>
        </div>
        <nav className="nav-bars">
          <div className="main-nav">
            {navigationItems && navigationItems.map((nav, index) => (
              <NavigationMenuItem navItem={nav} key={index} />
            ))}
          </div>
        </nav>
      </div>
    </div>
  );
}

SecondaryNavigationMobile.propTypes = {
  ...secondaryNavTypes,
}.isRequired;

SecondaryNavigationMobile.defaultProps = {
  secondaryNavStyle: '',
  title: '',
  navigationItems: [],
};
