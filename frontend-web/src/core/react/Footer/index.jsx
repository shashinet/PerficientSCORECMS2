/* eslint-disable max-len */
import React from 'react';

// Children
import SocialIconLIst from '../SocialIconList';
import Components from '../ComponentFactory/components';

// styles
import styles from './index.module.scss';

// types
import { footerDefaultTypes, footerTypes } from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function FooterNavigation(props) {
  const {
    upperFooter,
    lowerFooter,
    logoUrl,
    image,
    footerStyle,
    globalStyle,
  } = props;

  return (
    <>
      <div className={[styles.footer, ...footerStyle, ...globalStyle].join(' ')}>
        {upperFooter.length > 0 && (
          <div className="upper-footer">
            <div className="container">
              {React.Children.toArray(upperFooter.map((col) => Components(col)))}
            </div>
          </div>
        )}
        <div className="lower-footer">
          <div className="container">
            <div className="brand">
              {image.contentType === 'Image' ? (
                <div className="brand-logo">
                  <a href={logoUrl} className="brand-image-button">
                    <img src={image.imageSrc} alt={image.altText} />
                  </a>
                </div>
              ) : (
                <div className="brand-logo">
                  <a
                    href={logoUrl}
                    aria-label={image.altText}
                    title={image.altText}
                    className="brand-image-button"
                    dangerouslySetInnerHTML={{ __html: image.imageSrc }}
                  />
                </div>
              )}
              {lowerFooter.socialIcons && (
                <SocialIconLIst socialIcons={lowerFooter.socialIcons} />
              )}
            </div>
            <div className="lower-content">
              {React.Children.toArray(
                lowerFooter.lowerFooterContent.map((item) => (
                  <div className={item.displayOption}>
                    {item.columnContent.length > 0 && React.Children.toArray(item.columnContent.map((col) => Components(col)))}
                  </div>
                )),
              )}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

FooterNavigation.propTypes = {
  ...footerTypes,
};

FooterNavigation.defaultProps = {
  ...footerDefaultTypes,
};
