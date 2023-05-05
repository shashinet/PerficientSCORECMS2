import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function LanguageSelector(props) {
  const { block } = props;
  return (
    <a
      href={block.url}
      className={['nav-item-icon-link', ...block.globalStyle].join(' ')}
      aria-label={`${block.title} link`}
      tabIndex="0"
      rel="noreferrer"
    >
      {block.title}

      <span
        className="nav-item-icon"
        aria-label={block.image.altText}
        dangerouslySetInnerHTML={{ __html: block.image.imageSrc }}
      />
    </a>
  );
}

LanguageSelector.propTypes = {
  block: PropTypes.shape({
    url: PropTypes.string,
    globalStyle: PropTypes.arrayOf(PropTypes.string),
    title: PropTypes.string,
    image: PropTypes.shape({
      contentType: PropTypes.string,
      imageSrc: PropTypes.string,
      altText: PropTypes.string,
    }),
    contentType: PropTypes.string,
  }),
};

LanguageSelector.defaultProps = {
  block: {
    url: '',
    globalStyle: [],
    title: '',
    icon: '',
    contentType: '',
  },
};
