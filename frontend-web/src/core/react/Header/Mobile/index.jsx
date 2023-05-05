import React from 'react';

// children
import Components from '../../ComponentFactory/components';

// styles
import styles from './index.module.scss';

// types
import { mobileDefaultTypes, mobileTypes } from '../types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function MobileHeader(props) {
  const {
    lang,
    headerStyle,
    globalStyle,
    logoUrl,
    image,
    tagline,
    navigationItems,
    utilityNavigation,
  } = props;
  const [open, setOpen] = React.useState(false);
  const search = utilityNavigation.filter((item) => item.contentType === 'SearchButton');

  return (
    <div
      className={[styles.header, ...headerStyle, ...globalStyle].join(' ')}
      lang={lang}
    >
      <nav className="mega-menu" role="navigation" aria-label="Main Navigation">
        <div className="nav-wrapper">
          <div className={tagline ? 'header-area tagline' : 'header-area'}>
            <button
              className={open ? 'navbar-toggle open' : 'navbar-toggle'}
              type="button"
              onClick={() => setOpen(!open)}
            >
              <span className="visually-hidden">Toggle navigation</span>
              <span className="icon-bar" />
              <span className="icon-bar" />
              <span className="icon-bar" />
              <span className="toggle-text">
                {open ? 'exit' : 'menu'}
              </span>
            </button>
            <div className="brand">
              {image && (
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
              )}
              {tagline && <div className="tagline" dangerouslySetInnerHTML={{ __html: tagline }} />}
            </div>
          </div>
          <div className={open ? 'nav-bars slide-in' : 'nav-bars slide-out'}>
            {navigationItems && (
              <div className="main-nav">
                {search.length > 0
                  && Components(search[0])}
                {React.Children.toArray(navigationItems.map((block) => Components(block)))}
              </div>
            )}
            {utilityNavigation.length > 0 && (
              <div className="utility-nav">
                {React.Children.toArray(utilityNavigation.map((block) => (block.contentType !== 'SearchButton' && Components(block))))}
              </div>
            )}
          </div>
        </div>
      </nav>
    </div>
  );
}

MobileHeader.propTypes = {
  ...mobileTypes,
};

MobileHeader.defaultProps = {
  ...mobileDefaultTypes,
};
