import PropTypes from 'prop-types';

export const stripeTypes = {
  stripeStyles: PropTypes.arrayOf(PropTypes.string),
  headingAlign: PropTypes.string,
  ctaAlign: PropTypes.string,
  heading2: PropTypes.string,
  heading3: PropTypes.string,
  heading4: PropTypes.string,
  subtitle: PropTypes.string,
  children: PropTypes.oneOfType([PropTypes.arrayOf(PropTypes.node), PropTypes.node]),
  backgroundColor: PropTypes.string,
  backgroundImage: PropTypes.string,
  color: PropTypes.string,
  cta: PropTypes.oneOfType([PropTypes.arrayOf(PropTypes.node), PropTypes.node]),
  alignment: PropTypes.string,
};

export const stripeDefaultTypes = {
  stripeStyles: ['default'],
  backgroundColor: '',
  backgroundImage: '',
  color: '',
  cta: null,
  children: null,
  headingAlign: '',
  ctaAlign: '',
  subtitle: null,
  heading2: null,
  heading3: null,
  heading4: null,
  alignment: '',
};
