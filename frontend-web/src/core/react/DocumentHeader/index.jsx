import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function DocumentHeader(props) {
  const {
    documentHeaderStyles,
    h1,
    h2,
    imageSrc,
    altText,
  } = props;
  return (
    <div className={['score-document-header', ...documentHeaderStyles].join(' ')}>
      <div className="score-header-image ">
        <img src={imageSrc} alt={altText} className="score-image img-fluid" />
      </div>
      <h1>{h1}</h1>
      <h2>{h2}</h2>
    </div>
  );
}

DocumentHeader.propTypes = {
  documentHeaderStyles: PropTypes.arrayOf(PropTypes.string),
  h1: PropTypes.string,
  h2: PropTypes.string,
  imageSrc: PropTypes.string,
  altText: PropTypes.string,
};

DocumentHeader.defaultProps = {
  documentHeaderStyles: ['default'],
  h1: '',
  h2: '',
  imageSrc: '',
  altText: '',
};
