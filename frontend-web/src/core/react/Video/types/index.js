import PropTypes from 'prop-types';

const props = {
  videoStyles: PropTypes.arrayOf(PropTypes.string),
  contentType: PropTypes.string,
  videoSrc: PropTypes.string,
  videoThumb: PropTypes.string,
  videoTitle: PropTypes.string,
  videoType: PropTypes.string,
};

export default props;
