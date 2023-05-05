import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function NavigationLink(props) {
  const { block } = props;
  return (
    <a
      href={block.url}
      className={['nav-item-link', block.isActive ? 'active' : '', ...block.globalStyle].join(' ')}
      aria-label={`${block.title} link`}
      target={block.openInNewWindow ? '_blank' : '_self'}
      tabIndex="0"
      rel="noreferrer"
    >
      {block.title}
    </a>
  );
}

NavigationLink.propTypes = {
  block: PropTypes.shape({
    url: PropTypes.string,
    globalStyle: PropTypes.arrayOf(PropTypes.string),
    title: PropTypes.string,
    openInNewWindow: PropTypes.bool,
    isActive: PropTypes.bool,
  }),
};

NavigationLink.defaultProps = {
  block: {
    url: '',
    globalStyle: [],
    title: '',
    openInNewWindow: false,
    isActive: false,
  },
};
