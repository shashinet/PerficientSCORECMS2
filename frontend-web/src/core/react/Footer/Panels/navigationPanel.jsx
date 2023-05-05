import React from 'react';
import PropTypes from 'prop-types';
// eslint-disable-next-line import/no-cycle
import Components from '../../ComponentFactory/components';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function NavigationPanel(props) {
  const { block } = props;
  return (
    <div className={['footer-col', `${block.displayOption}`].join(' ')}>
      {React.Children.toArray(block.columnContent.map((col) => Components(col)))}
    </div>
  );
}

NavigationPanel.propTypes = {
  block: PropTypes.shape({
    displayOption: PropTypes.string,
    columnContent: PropTypes.arrayOf(PropTypes.shape({})),
  }),
};

NavigationPanel.defaultProps = {
  block: {},
};
