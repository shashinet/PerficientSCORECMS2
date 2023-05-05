/* eslint-disable no-template-curly-in-string */
import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function PictureStripe(props) {
  const {
    backgroundColor,
    backgroundImage,
    fallBackSrc,
    children,
    color,
    subtitle,
    heading2,
    heading3,
    heading4,
    pictureStripeStyles,
    cta,
    alignment,
    headingAlign,
    ctaAlign,
  } = props;
  return (
    <section
      className={['score-stripe score-picture-stripe', ...pictureStripeStyles].join(' ')}
      style={{
        backgroundColor,
        color,
      }}
    >
      {backgroundImage && (
        <picture>
          {backgroundImage.map((image, index) => (
            <source
              key={`${image.srcSet}-${index}`}
              media={`(min-width: ${image.minWidth}px)`}
              srcSet={image.srcSet}
              type={image.type}
            />
          ))}
          <img src={fallBackSrc} alt="alt text area" />
        </picture>
      )}
      <div className={['container', `${alignment}`].join(' ')}>
        <div className="w-full">
          <div className={['section-header', `${headingAlign}`].join(' ')}>
            {heading2 && (<h2>{heading2}</h2>)}
            {heading3 && (<h3>{heading3}</h3>)}
            {heading4 && (<h4>{heading4}</h4>)}
            {subtitle && <p>{subtitle}</p>}
          </div>
          <div className="content-area">{children}</div>
          {cta && <div className={['cta', `${ctaAlign}`].join(' ')}>{cta}</div>}
        </div>
      </div>
    </section>
  );
}

PictureStripe.propTypes = {
  pictureStripeStyles: PropTypes.arrayOf(PropTypes.string),
  heading2: PropTypes.string,
  heading3: PropTypes.string,
  heading4: PropTypes.string,
  subtitle: PropTypes.string,
  children: PropTypes.oneOfType([PropTypes.arrayOf(PropTypes.node), PropTypes.node]),
  backgroundColor: PropTypes.string,
  backgroundImage: PropTypes.arrayOf(PropTypes.shape({
    alt: PropTypes.string,
    srcSet: PropTypes.string,
    media: PropTypes.string,
    minWidth: PropTypes.string,
    type: PropTypes.string,
  })),
  fallBackSrc: PropTypes.string,
  color: PropTypes.string,
  cta: PropTypes.oneOfType([PropTypes.arrayOf(PropTypes.node), PropTypes.node]),
  alignment: PropTypes.string,
  headingAlign: PropTypes.string,
  ctaAlign: PropTypes.string,
};

PictureStripe.defaultProps = {
  pictureStripeStyles: ['default'],
  backgroundColor: '',
  backgroundImage: [{
    src: '',
    alt: '',
    srcSet: '',
    minWidth: '',
    type: '',
  }],
  fallBackSrc: '',
  color: '',
  cta: null,
  children: null,
  subtitle: null,
  heading2: null,
  heading3: null,
  heading4: null,
  alignment: '',
  headingAlign: '',
  ctaAlign: '',
};
