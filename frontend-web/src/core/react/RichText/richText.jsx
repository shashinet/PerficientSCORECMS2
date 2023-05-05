import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function RichText(props) {
  const {
    richTextStyles,
    children,
  } = props;
  return (
    <div className={['rich-text', ...richTextStyles].join(' ')}>
      <div className="rich-text" dangerouslySetInnerHTML={{ __html: children }} />
    </div>
  );
}

RichText.propTypes = {
  richTextStyles: PropTypes.arrayOf(PropTypes.string),
  children: PropTypes.node,
};
RichText.defaultProps = {
  richTextStyles: ['default'],
  children: null,
};
