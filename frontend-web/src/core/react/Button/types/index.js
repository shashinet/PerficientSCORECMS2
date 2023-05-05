import PropTypes from 'prop-types';

const props = {
  buttonStyles: PropTypes.arrayOf(PropTypes.string),
  size: PropTypes.string,
  title: PropTypes.string,
  preText: PropTypes.node,
  postText: PropTypes.node,
  onClick: PropTypes.string,
  items: PropTypes.shape({}),
  block: PropTypes.shape({}),
};

export default props;
