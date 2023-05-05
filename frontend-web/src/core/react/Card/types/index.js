import PropTypes from 'prop-types';

const props = {
  cardStyles: PropTypes.arrayOf(PropTypes.string),
  subHeading: PropTypes.string,
  h2: PropTypes.string,
  h3: PropTypes.string,
  h4: PropTypes.string,
  h5: PropTypes.string,
  image: PropTypes.shape({
    imageSrc: PropTypes.string,
    altText: PropTypes.string,
    contentType: PropTypes.string,
  }),
  body: PropTypes.string,
  cta: PropTypes.oneOfType([PropTypes.arrayOf(PropTypes.node), PropTypes.node]),
};

export default props;
