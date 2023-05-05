import React from 'react';
import PropTypes from 'prop-types';

// children
import MobileTabPanel from '../../Panels';

// styles
import styles from './index.module.scss';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function LargeButtonTabMobile(props) {
  const {
    panels,
  } = props;

  return (
    <div className={[styles.mobileButtonTabs, 'tabs'].join(' ')}>
      {React.Children.toArray(panels.map((item) => (
        <MobileTabPanel block={item} />
      )))}
    </div>
  );
}

LargeButtonTabMobile.propTypes = {
  panels: PropTypes.arrayOf(
    PropTypes.shape({}),
  ),
};

LargeButtonTabMobile.defaultProps = {
  panels: [],
};
