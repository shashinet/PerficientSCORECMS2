import PropTypes from 'prop-types';

export const verticalTabsetTypes = {
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

export const verticalTabsetDefaultTypes = {
  globalStyle: ['default'],
  title: '',
  subTitle: '',
  titleTag: '',
  subTitleTag: '',
  panels: [{}],
  callToActionButtons: [],
};

export const verticalTabsetDesktopTypes = {
  subTitle: PropTypes.string,
  subTitleTag: PropTypes.string,
  panels: PropTypes.arrayOf(
    PropTypes.shape({
      image: PropTypes.shape({
        imageSrc: PropTypes.string,
        altText: PropTypes.string,
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
  ),
  callToActionButtons: PropTypes.arrayOf(PropTypes.shape({})),
};

export const verticalTabsetDesktopDefaultTypes = {
  subTitle: '',
  subTitleTag: '',
  panels: [{}],
  callToActionButtons: [],
};
