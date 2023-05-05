import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function InfoCard(props) {
  const { block } = props;

  return (
    <>
      <div className={[...block.cardStyles].join(' ')}>
        {block.image.imageSrc && (
          <>
            {block.image.contentType === 'Image' ? (
              <div className="image-wrapper">
                <img
                  src={block.image.imageSrc}
                  alt={block.image.altText}
                  // eslint-disable-next-line react/no-unknown-property
                  contentType={block.image.contentType}
                />
              </div>
            ) : (
              <div className="image-wrapper" dangerouslySetInnerHTML={{ __html: block.image.imageSrc }} />
            )}
          </>
        )}
        <div className="caption">
          {block.heading && <h5>{block.heading}</h5>}
          {block.text && <span dangerouslySetInnerHTML={{ __html: block.text }} />}
        </div>
      </div>
    </>
  );
}

InfoCard.propTypes = {
  block: PropTypes.shape({
    cardStyles: PropTypes.arrayOf(PropTypes.string),
    heading: PropTypes.string,
    text: PropTypes.string,
    contentType: PropTypes.string,
    image: PropTypes.shape({
      imageSrc: PropTypes.string,
      contentType: PropTypes.string,
      altText: PropTypes.string,
    }),
  }),
};

InfoCard.defaultProps = {
  block: {
    cardStyles: [],
    heading: '',
    text: '',
    contentType: '',
    image: {
      imageSrc: '',
      contentType: '',
      altText: '',
    },
  },
};
