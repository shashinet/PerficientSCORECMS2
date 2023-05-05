import PropTypes from 'prop-types';

export const secondaryNavTypes = {
  secondaryNavStyle: PropTypes.string,
  title: PropTypes.string,
  navigationItems: PropTypes.arrayOf(PropTypes.shape({})),
  infoPanel: PropTypes.arrayOf(PropTypes.shape({})),
  buttonText: PropTypes.string,
  buttonStyle: PropTypes.string,
};

export const secondaryNavDefaultTypes = {
  secondaryNavStyle: '',
  title: '',
  navigationItems: [],
  buttonText: '',
  buttonStyle: '',
};
