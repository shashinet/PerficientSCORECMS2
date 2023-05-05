import React from 'react';
import PropTypes from 'prop-types';
import AccordionPanel from './Panel';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function Accordions(props) {
  const {
    accordionStyles,
    panel,
  } = props;

  return (
    <div className={['score-accordion', ...accordionStyles].join(' ')}>
      {React.Children.toArray(panel.map((item) => (
        <AccordionPanel
          title={item.title}
          panelContent={item.panelContent}
        />
      )))}
    </div>
  );
}

Accordions.propTypes = {
  accordionStyles: PropTypes.arrayOf(PropTypes.string),
  panel: PropTypes.arrayOf(PropTypes.shape({
    title: PropTypes.string,
    panelContent: PropTypes.string,
  })),
};

Accordions.defaultProps = {
  accordionStyles: ['default'],
  panel: [],
};
