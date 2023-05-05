import React from 'react';
import types from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function Card(props) {
  if (Object.keys(props).includes('block')) {
    // eslint-disable-next-line no-param-reassign, react/destructuring-assignment
    props = props.block;
  }

  const {
    cardStyles = ['default'],
    subHeading = '',
    h2 = '',
    h3 = '',
    h4 = '',
    h5 = '',
    image = {
      imageSrc: '',
      altText: '',
      contentType: '',
    },
    body = null,
    cta = null } = props;

  return (
    <div className={['score-card', ...cardStyles].join(' ')}>
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
        {subHeading && <span className="sub-heading">{subHeading}</span>}
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

Card.propTypes = {
  ...types,
};
