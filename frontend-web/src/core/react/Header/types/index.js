import PropTypes from 'prop-types';

export const headerTypes = {
  language: PropTypes.string,
  headerStyle: PropTypes.arrayOf(PropTypes.string),
  globalStyle: PropTypes.arrayOf(PropTypes.string),
  logoUrl: PropTypes.string,
  image: PropTypes.shape({
    contentType: PropTypes.string,
    imageSrc: PropTypes.string,
    altText: PropTypes.string,
  }),
  tagline: PropTypes.string,
  navigationItems: PropTypes.arrayOf(PropTypes.shape({})),
  utilityNavigation: PropTypes.arrayOf(PropTypes.shape({})),
  skipNavigation: PropTypes.string,
};

export const headerDefaultTypes = {
  language: '',
  headerStyle: [],
  globalStyle: [],
  logoUrl: '',
  image: {
    contentType: '',
    altText: 'Brand logo icon',
    imageSrc: '',
  },
  tagline: '',
  navigationItems: [],
  utilityNavigation: [],
  skipNavigation: '',
};

export const desktopTypes = {
  language: PropTypes.string,
  headerStyle: PropTypes.arrayOf(PropTypes.string),
  globalStyle: PropTypes.arrayOf(PropTypes.string),
  logoUrl: PropTypes.string,
  image: PropTypes.shape({
    imageSrc: PropTypes.string,
    altText: PropTypes.string,
    contentType: PropTypes.string,
  }),
  tagline: PropTypes.string,
  contentType: PropTypes.string,
  id: PropTypes.string,
  navigationItems: PropTypes.arrayOf(PropTypes.shape({})),
  utilityNavigation: PropTypes.arrayOf(PropTypes.shape({})),
  skipNavigation: PropTypes.string,
};

export const desktopDefaultTypes = {
  language: '',
  headerStyle: [],
  globalStyle: [],
  logoUrl: '',
  image: {
    imageSrc: '',
    altText: 'brand logo icon',
    contentType: '',
  },
  tagline: '',
  contentType: '',
  id: '',
  navigationItems: [],
  utilityNavigation: [],
  skipNavigation: null,
};

export const mobileTypes = {
  lang: PropTypes.string,
  headerStyle: PropTypes.arrayOf(PropTypes.string),
  globalStyle: PropTypes.arrayOf(PropTypes.string),
  logoUrl: PropTypes.string,
  image: PropTypes.shape({
    contentType: PropTypes.string,
    imageSrc: PropTypes.string,
    altText: PropTypes.string,
  }),
  tagline: PropTypes.string,
  navigationItems: PropTypes.arrayOf(PropTypes.shape({})),
  utilityNavigation: PropTypes.arrayOf(PropTypes.shape({})),
};

export const mobileDefaultTypes = {
  lang: '',
  headerStyle: [],
  globalStyle: [],
  logoUrl: '',
  image: {
    contentType: '',
    imageSrc: '',
    altText: 'brand logo icon',
  },
  tagline: '',
  navigationItems: [],
  utilityNavigation: [],
};
