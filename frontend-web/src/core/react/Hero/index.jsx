import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function Hero(props) {
  const {
    heroStyles,
    h1,
    h2,
    imageSrc,
    altText,
    backgroundImage,
    color,
    backgroundColor,
    body,
    cta,
  } = props;
  return (
    <div
      className={['score-hero', ...heroStyles].join(' ')}
      style={{
        backgroundImage: `url(${backgroundImage})`,
        backgroundColor,
        color,
      }}
    >
      {imageSrc && (
        <div className="score-hero-image">
          <img src={imageSrc} alt={altText} className="score-image img-fluid" />
        </div>
      )}
      <div className="caption">
        <h1>{h1}</h1>
        {h2 && <h2>{h2}</h2>}
        {body && <div className="score-hero-body" dangerouslySetInnerHTML={{ __html: body }} />}
        {cta && <div className="score-call-to-action">{cta}</div>}
      </div>
    </div>
  );
}

Hero.propTypes = {
  heroStyles: PropTypes.arrayOf(PropTypes.string),
  h1: PropTypes.string.isRequired,
  h2: PropTypes.string,
  imageSrc: PropTypes.string,
  altText: PropTypes.string,
  backgroundColor: PropTypes.string,
  backgroundImage: PropTypes.string,
  color: PropTypes.string,
  body: PropTypes.string,
  cta: PropTypes.oneOfType([PropTypes.arrayOf(PropTypes.node), PropTypes.node]),
};

Hero.defaultProps = {
  heroStyles: ['default'],
  h2: '',
  imageSrc: '',
  altText: '',
  backgroundColor: '',
  backgroundImage: '',
  color: '',
  body: '',
  cta: null,
};
