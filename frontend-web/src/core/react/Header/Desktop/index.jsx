import React from 'react';

// children
import Components from '../../ComponentFactory/components';

// styles
import styles from './index.module.scss';

// types
import { desktopDefaultTypes, desktopTypes } from '../types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function DesktopHeader(props) {
  const {
    language,
    headerStyle,
    globalStyle,
    logoUrl,
    image,
    tagline,
    navigationItems,
    utilityNavigation,
    id,
    contentType,
    skipNavigation,
  } = props;

  return (
    <div
      className={[styles.header, id, ...headerStyle, ...globalStyle, language].join(' ')}
      data-language={contentType}
    >
      <nav className="mega-menu container" role="navigation" aria-label="Main Navigation">
        <div className="nav-wrapper">
          {skipNavigation && (
            <a href="dev/src/Web/wwwroot/src/core/react/Header/Desktop/index.jsx" className="skip-to-content">
              {skipNavigation}
            </a>
          )}
          <div className="header-area">
            <div className="brand">
              <div className="brand-logo">
                {image.contentType === 'Image' ? (
                  <a href={logoUrl} className="brand-image-button">
                    <img src={image.imageSrc} alt={image.altText} />
                  </a>
                ) : (
                  <a
                    href={logoUrl}
                    aria-label={image.altText}
                    title={image.altText}
                    className="brand-image-button"
                    dangerouslySetInnerHTML={{ __html: image.imageSrc }}
                  />
                )}
              </div>
              {tagline && <div className="tagline" dangerouslySetInnerHTML={{ __html: tagline }} />}
            </div>
          </div>
          <div className="nav-bars">
            {navigationItems.length > 0 && (
              <div className="main-nav">
                {React.Children.toArray(navigationItems.map((block) => Components(block)))}
              </div>
            )}
            {utilityNavigation.length > 0 && (
              <div className="utility-nav">
                {React.Children.toArray(utilityNavigation.map((block) => Components(block)))}
              </div>
            )}
          </div>
        </div>
      </nav>
    </div>
  );
}

DesktopHeader.propTypes = {
  ...desktopTypes,
};

DesktopHeader.defaultProps = {
  ...desktopDefaultTypes,
};
