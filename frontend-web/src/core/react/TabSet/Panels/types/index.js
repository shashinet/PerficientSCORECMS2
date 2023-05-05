import PropTypes from 'prop-types';

export const mobileTabPanelTypes = {
  block: PropTypes.shape({
    image: PropTypes.shape({
      imageSrc: PropTypes.string,
      altText: PropTypes.string,
      original: PropTypes.shape({
        imageSrc: PropTypes.string,
        altText: PropTypes.string,
        contentType: PropTypes.string,
      }),
      croppings: PropTypes.arrayOf(PropTypes.shape({
        imageSrc: PropTypes.string,
        srcSet: PropTypes.string,
        contentType: PropTypes.string,
      })),
      contentType: PropTypes.string,
    }),
    buttonText: PropTypes.string,
    title: PropTypes.string,
    text: PropTypes.string,
    callToActionButtons: PropTypes.arrayOf(PropTypes.shape({
      style: PropTypes.string,
      text: PropTypes.string,
      url: PropTypes.string,
      openInNewWindow: PropTypes.bool,
      globalStyle: PropTypes.arrayOf(PropTypes.string),
    })),
  }),
};

export const mobileTabPanelDefaultTypes = {
  block: {},
};
