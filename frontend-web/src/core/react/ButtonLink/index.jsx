import React from 'react';
import Proptypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function ButtonLink(props) {
  const {
    buttonStyles,
    size,
    url,
    title,
  } = props;
  return (
    <a
      href={url}
      className={['score-button', ...buttonStyles, `${size}`].join(' ')}
      aria-label={title}
    >
      {title}
    </a>
  );
}

ButtonLink.propTypes = {
  buttonStyles: Proptypes.arrayOf(Proptypes.string),
  size: Proptypes.string,
  title: Proptypes.string,
  url: Proptypes.string,
};

ButtonLink.defaultProps = {
  buttonStyles: ['primary'],
  size: '',
  url: '',
  title: '',
};
