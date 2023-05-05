import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function PricingCard(props) {
  const {
    PricingCardStyles,
    h2,
    h3,
    h4,
    h5,
    price,
    priceTerm,
    image,
    body,
    cta,
  } = props;
  return (
    <div className={['score-price-card', ...PricingCardStyles].join(' ')}>
      {image.imageSrc && (
        <>
          {image.contentType === 'Image' ? (
            <div className="image-wrapper">
              <img src={image.imageSrc} alt={image.altText} className="score-image img-fluid" />
            </div>
          ) : (
            <div className="image-wrapper" dangerouslySetInnerHTML={{ __html: image.imageSrc }} />
          )}
        </>
      )}
      <div className="caption">
        {h2 && <h2>{h2}</h2>}
        {h3 && <h3>{h3}</h3>}
        {h4 && <h4>{h4}</h4>}
        {h5 && <h5>{h5}</h5>}
        {price && <div className="price">{price}</div>}
        {priceTerm && <div className="price-term">{priceTerm}</div>}
        {body && (
          <div className="score-card-body">
            <div className="rich-text" dangerouslySetInnerHTML={{ __html: body }} />
          </div>
        )}
        {cta && <div className="score-call-to-action">{cta}</div>}
      </div>
    </div>
  );
}

PricingCard.propTypes = {
  PricingCardStyles: PropTypes.arrayOf(PropTypes.string),
  h2: PropTypes.string,
  h3: PropTypes.string,
  h4: PropTypes.string,
  h5: PropTypes.string,
  price: PropTypes.string,
  priceTerm: PropTypes.string,
  image: PropTypes.shape({
    imageSrc: PropTypes.string,
    altText: PropTypes.string,
    contentType: PropTypes.string,
  }),
  body: PropTypes.string,
  cta: PropTypes.oneOfType([PropTypes.arrayOf(PropTypes.node), PropTypes.node]),
};

PricingCard.defaultProps = {
  PricingCardStyles: ['default'],
  h2: '',
  h3: '',
  h4: '',
  h5: '',
  price: '',
  priceTerm: '',
  image: {
    imageSrc: '',
    altText: '',
    contentType: '',
  },
  body: null,
  cta: null,
};
