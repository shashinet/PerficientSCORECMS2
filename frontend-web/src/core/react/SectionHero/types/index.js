import PropTypes from 'prop-types';

const props = {
  sectionHeroStyles: PropTypes.arrayOf(PropTypes.string),
  h2: PropTypes.string,
  h3: PropTypes.string,
  h4: PropTypes.string,
  contentType: PropTypes.string,
  imageSrc: PropTypes.string,
  imageAlt: PropTypes.string,
  videoSrc: PropTypes.string,
  videoThumb: PropTypes.string,
  videoTitle: PropTypes.string,
  videoType: PropTypes.string,
  cta: PropTypes.oneOfType([PropTypes.arrayOf(PropTypes.node), PropTypes.node]),
  body: PropTypes.string,
};

export default props;
