import React from 'react';
import PropTypes from 'prop-types';

// styles
import styles from './index.module.scss';

/**
 *
 * @param heroImage
 * @param name
 * @param location
 * @param ctaUrl
 * @param ctaText
 * @param heroStyles
 * @returns {JSX.Element}
 * @constructor
 */
export default function HeroBanner({
  heroImage,
  name,
  location,
  ctaUrl,
  ctaText,
  heroStyles,
}) {
  return (
    <div className={[styles.banner, ...heroStyles].join(' ')}>
      <div className="container">
        {heroImage && (
          <div className="image-wrapper">
            <picture>
              {React.Children.toArray(heroImage.croppings && heroImage.croppings.map((src) => (
                <source srcSet={src.imageSrc} media={src.srcSet} />
              )))}
              {heroImage.original && (
                <img
                  src={heroImage.original.imageSrc}
                  alt={heroImage.original.altText}
                />
              )}
            </picture>
          </div>
        )}
        <div className="caption">
          <span className="heading1">{name}</span>
          <span className="subtitle">{location}</span>
          <div className="cta-area">
            <a href={ctaUrl} className="score-button primary">{ctaText}</a>
          </div>
        </div>
      </div>
    </div>
  );
}

HeroBanner.propTypes = {
  heroStyles: PropTypes.arrayOf(PropTypes.string),
  heroImage: PropTypes.shape({
    original: PropTypes.shape({
      imageSrc: PropTypes.string,
      altText: PropTypes.string,
      contentType: PropTypes.string,
    }),
    croppings: PropTypes.arrayOf(PropTypes.shape({
      imageSrc: PropTypes.string,
      srcSet: PropTypes.string,
      contentType: PropTypes.string,
    })),
  }),
  name: PropTypes.string,
  location: PropTypes.string,
  ctaText: PropTypes.string,
  ctaUrl: PropTypes.string,

}.isRequired;

HeroBanner.defaultProps = {
  heroStyles: ['on-page'],
  heroImage: {},
  name: '',
  location: '',
  ctaText: '',
  ctaUrl: '',
};
