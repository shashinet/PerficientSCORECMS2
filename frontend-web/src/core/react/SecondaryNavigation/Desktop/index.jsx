import React from 'react';

// children
import NavigationMenuItem from '../MenuItem';
import Components from '../../ComponentFactory/components';
import BackToTop from '../../../../global/components/BackToTop/backToTop';

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
export default function SecondaryNavigationDesktop(props) {
  const { title, secondaryNavStyle, navigationItems, buttonText, infoPanel } = props;

  return (
    <div className={[...secondaryNavStyle, styles.secondaryNavigationWrapper].join(' ')}>
      <div className="mega-menu secondary-navigation" aria-label="secondary-navigation">
        <div id="header" className="header-area">
          <h5>{title}</h5>
        </div>
        <nav className="nav-bars">
          <div className="main-nav">
            {navigationItems
              // eslint-disable-next-line max-len
              && navigationItems.map((nav, index) => <NavigationMenuItem navItem={nav} key={index} />)}
          </div>
          {infoPanel.length > 0
            && React.Children.toArray(infoPanel.map((block) => Components(block)))}
          <div className="cta">
            <BackToTop href="#header" aria-label={buttonText} title={buttonText} />
          </div>
        </nav>
      </div>
    </div>
  );
}

SecondaryNavigationDesktop.propTypes = {
  ...secondaryNavTypes,
}.isRequired;

SecondaryNavigationDesktop.defaultProps = {
  language: '',
  secondaryNavStyle: '',
  title: '',
  navigationItems: [],
  buttonText: '',
  infoPanel: [],
};
