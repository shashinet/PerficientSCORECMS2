import React from 'react';
import Proptypes from 'prop-types';

export default function BackToTop(props) {
  const {
    href,
    title,
    ariaLabel,
    image,
  } = props;
  return (
    <a
      href={href}
      className="back-to-top"
      aria-label={ariaLabel}
    >
      {image}
      <span>{title}</span>
    </a>
  );
}

BackToTop.propTypes = {
  title: Proptypes.string,
  href: Proptypes.string,
  ariaLabel: Proptypes.string,
  image: Proptypes.node,
};

BackToTop.defaultProps = {
  href: '',
  title: '',
  ariaLabel: '',
  image: (
    <>
      <svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path
          d="M16 31.5C7.43959 31.5 0.5 24.5604 0.5 16C0.5 7.43959 7.43959 0.5 16 0.5C24.5604 0.5 31.5 7.43959 31.5 16C31.5 24.5604 24.5604 31.5 16 31.5Z"
          fill="none"
          stroke="#222222"
        />
        <line x1="16" y1="8.50049" x2="16" y2="23.5005" stroke="#512D6D" strokeLinecap="round" />
        <path
          fillRule="evenodd"
          clipRule="evenodd"
          d="M22.8447 13.8445C23.0204 14.0279 23.0503 14.3068 22.9176 14.5233C22.726 14.8358 22.2907 14.8825 22.0373 14.6179L15.9336 8.24544L9.82995 14.6179C9.57646 14.8825 9.14116 14.8358 8.94963 14.5233C8.81688 14.3068 8.84682 14.0279 9.02252 13.8445L15.3318 7.25739C15.6599 6.91485 16.2073 6.91485 16.5354 7.25739L22.8447 13.8445Z"
          fill="#512D6D"
        />
      </svg>
    </>
  ),
};
