import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function Tile(props) {
  const { block } = props;
  return (
    <div className={['score-tile', ...block.tileStyles].join(' ')}>
      {block.image && (
        <>
          {block.image.contentType === 'Image' ? (
            <div className="img-wrapper">
              <img src={block.image.imageSrc} alt={block.image.altText} />
            </div>
          ) : (
            <div className="img-wrapper" dangerouslySetInnerHTML={{ __html: block.image.imageSrc }} />
          )}
        </>
      )}
      {block.title && <span className="tile-title">{block.title}</span>}
      {block.body && (
        <div className="tile-body">
          <div className="rich-text" dangerouslySetInnerHTML={{ __html: block.body }} />
        </div>
      )}
      {block.href && (
        <a
          href={block.href}
          className="clickable-area"
          aria-label={`${block.title} link`}
          target={`${block.openInNewWindow ? '_blank' : '_self'}`}
        >
          <span className="visually-hidden">{block.title}</span>
        </a>
      )}
    </div>
  );
}

Tile.propTypes = {
  block: PropTypes.shape({
    tileStyles: PropTypes.arrayOf(PropTypes.string),
    href: PropTypes.string,
    image: PropTypes.shape({
      imageSrc: PropTypes.string,
      altText: PropTypes.string,
      contentType: PropTypes.string,
    }),
    title: PropTypes.string,
    body: PropTypes.string,
    openInNewWindow: PropTypes.bool,
  }),
};

Tile.defaultProps = {
  block: {
    tileStyles: ['default'],
    href: '',
    image: {},
    title: '',
    body: null,
    openInNewWindow: false,
  },
};
