import React from 'react';
import types from './types';
/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function Button(props) {
  if (Object.keys(props).includes('block')) {
    // eslint-disable-next-line no-param-reassign, react/destructuring-assignment
    props = props.block;
  }

  const {
    buttonStyles = ['primary'],
    size = 'md',
    preText = '',
    title = '',
    postText = '',
    onClick = null } = props;

  return (
    <button
      type="button"
      className={['score-button', ...buttonStyles, `${size}`].join(' ')}
      aria-label={title}
      onClick={onClick}
    >
      {preText}
      {title}
      {postText}
    </button>
  );
}

Button.propTypes = {
  ...types,
};
