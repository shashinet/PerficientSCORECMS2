import PropTypes from 'prop-types';

export const footerTypes = {
  logoUrl: PropTypes.string,
  image: PropTypes.shape({
    imageSrc: PropTypes.string,
    altText: PropTypes.string,
    contentType: PropTypes.string,
  }),
  footerStyle: PropTypes.arrayOf(PropTypes.string),
  globalStyle: PropTypes.arrayOf(PropTypes.string),
  upperFooter: PropTypes.arrayOf(PropTypes.shape({})),
  lowerFooter: PropTypes.shape({
    socialIcons: PropTypes.arrayOf(
      PropTypes.shape({
        title: PropTypes.string,
        imageSrc: PropTypes.string,
        href: PropTypes.string,
        openInNewWindow: PropTypes.bool,
      }),
    ),
    lowerFooterContent: PropTypes.arrayOf(PropTypes.shape({})),
  }),
};

export const footerDefaultTypes = {
  logoUrl: '',
  image: {
    altText: '',
    contentType: '',
    imageSrc: '',
  },
  footerStyle: [],
  globalStyle: [],
  upperFooter: [],
  lowerFooter: {
    socialIcons: [],
    lowerFooterContent: {},
  },
};

export const footerMenuTypes = {
  title: PropTypes.string.isRequired,
  link: PropTypes.string,
  linkAriaLebel: PropTypes.string.isRequired,
  openInNewWindow: PropTypes.bool.isRequired,
  childPages: PropTypes.arrayOf(
    PropTypes.shape({
      highlighted: PropTypes.bool,
      title: PropTypes.string,
      linkAriaLebel: PropTypes.string,
      link: PropTypes.string,
      openInNewWindow: PropTypes.bool,
    }),
  ),
};

export const footerMenuDefaultTypes = {
  link: '',
  childPages: [],
};
