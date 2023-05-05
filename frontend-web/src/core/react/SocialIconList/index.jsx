import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param socialIcons
 * @returns {JSX.Element}
 * @constructor
 */
export default function SocialIconLIst({ socialIcons }) {
  return (
    <div className="social">
      <ul className="social-list">
        {React.Children.toArray(
          socialIcons.map((item) => (
            <li className="social-list-item">
              {item.image.contentType === 'Image' ? (
                <a
                  href={item.url}
                  target={`${item.openInNewWindow ? '_blank' : '_self'}`}
                  aria-label={item.title}
                >
                  <img src={item.image.imageSrc} alt={`${item.image.altText} icon`} />
                </a>
              ) : (
                <a
                  href={item.url}
                  target={`${item.openInNewWindow ? '_blank' : '_self'}`}
                  aria-label={item.image.altText}
                  dangerouslySetInnerHTML={{ __html: item.image.imageSrc }}
                />
              )}
            </li>
          )),
        )}
      </ul>
    </div>
  );
}

SocialIconLIst.propTypes = {
  socialIcons: PropTypes.arrayOf(
    PropTypes.shape({
      image: PropTypes.shape({
        contentType: PropTypes.string,
        imageSrc: PropTypes.string,
        altText: PropTypes.string,
      }),
      url: PropTypes.string,
      openInNewWindow: PropTypes.bool,
      title: PropTypes.string,
    }),
  ),
};

SocialIconLIst.defaultProps = {
  socialIcons: [],
};
