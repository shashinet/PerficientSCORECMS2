import React from 'react';
import types from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function SectionHeader(props) {
  if (Object.keys(props).includes('block')) {
    // eslint-disable-next-line no-param-reassign, react/destructuring-assignment
    props = props.block;
  }

  const {
    sectionHeaderStyles = ['default'],
    h2 = '',
    h3 = '',
    h4 = '',
    imageSrc = '',
    altText = '' } = props;

  return (
    <div className={['score-section-header', ...sectionHeaderStyles].join(' ')}>
      {imageSrc && (
        <div className="score-hero-image">
          <img src={imageSrc} alt={altText} className="score-image img-fluid" />
        </div>
      )}
      <h2>{h2}</h2>
      {h3 && <h3>{h3}</h3>}
      {h4 && <h4>{h4}</h4>}
    </div>
  );
}

SectionHeader.propTypes = {
  ...types,
};
