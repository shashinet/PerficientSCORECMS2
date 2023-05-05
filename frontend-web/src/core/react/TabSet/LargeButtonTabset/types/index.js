import PropTypes from 'prop-types';

export const largeButtonTabsetTypes = {
  globalStyle: PropTypes.arrayOf(PropTypes.string),
  title: PropTypes.string,
  subTitle: PropTypes.string,
  titleTag: PropTypes.string,
  subTitleTag: PropTypes.string,
  panels: PropTypes.arrayOf(
    PropTypes.shape({}),
  ),
  callToActionButtons: PropTypes.arrayOf(PropTypes.shape({})),
};

export const largeButtonTabsetDefaultTypes = {
  globalStyle: ['default'],
  title: '',
  subTitle: '',
  titleTag: '',
  subTitleTag: '',
  panels: [{}],
  callToActionButtons: [],
};

export const largeButtonTabsetDesktopTypes = {
  panels: PropTypes.arrayOf(
    PropTypes.shape({
      image: PropTypes.shape({
        imageSrc: PropTypes.string,
        altText: PropTypes.string,
        contentType: PropTypes.string,
      }),
      title: PropTypes.string,
      buttonText: PropTypes.string,
      text: PropTypes.string,
      callToActionButtons: PropTypes.arrayOf(PropTypes.shape({
        style: PropTypes.string,
        text: PropTypes.string,
        url: PropTypes.string,
        openInNewWindow: PropTypes.bool,
        globalStyle: PropTypes.arrayOf(PropTypes.string),
      })),
    }),
  ),
};

export const largeButtonTabsetDesktopDefaultTypes = {
  panels: [{
    image: {
      imageSrc: null,
      altText: null,
      contentType: null,
    },
    title: null,
    buttonText: null,
    text: null,
    callToActionButtons: [{
      style: null,
      text: null,
      url: null,
      openInNewWindow: false,
      globalStyle: [],
    }],
  }],
};
