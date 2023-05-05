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

export default function Tabset(props) {
  const {
    tabStyles,
    tabs,
  } = props;
  const [open, setOpen] = React.useState(tabs[0].label);
  const keyEvent = (e) => {
    if (e.key === 'Enter') {
      setOpen(e.target.innerText);
    }
  };

  return (
    <div className={['score-tab', ...tabStyles].join(' ')}>
      <div className="tab-buttons-wrapper">
        <>
          {React.Children.toArray(tabs.map((tabButton) => (
            <button
              key={tabButton.label}
              type="button"
              id={`tab-${tabButton.label.split(/\s/)
                .join('')}`}
              aria-controls={tabButton.label}
              className={[open === tabButton.label ? 'active' : 'out', 'tab-buttonLink'].join(' ')}
              data-id={tabButton.label.split(/\s/)
                .join('')}
              onClick={() => setOpen(tabButton.label)}
              onKeyUp={keyEvent}
            >
              {tabButton.label}
            </button>
          )))}
        </>
      </div>
      <div className="tab-content">
        <>
          {React.Children.toArray(tabs.map((tabContent) => (
            <div
              className={[open === tabContent.label ? 'active' : 'fade-out', 'content'].join(' ')}
              id={tabContent.label.split(/\s/)
                .join('')}
              role="tabpanel"
              key={tabContent.label}
              aria-labelledby={`tab-${tabContent.label.split(/\s/)
                .join('')}`}
            >
              {React.Children.toArray(tabContent.innerContent.map((item) => Components(item)))}
            </div>
          )))}
        </>
      </div>
    </div>
  );
}

Tabset.propTypes = {
  tabStyles: PropTypes.arrayOf(PropTypes.string),
  tabs: PropTypes.arrayOf(
    PropTypes.shape({
      label: PropTypes.string,
      innerContent: PropTypes.arrayOf(
        PropTypes.shape({
          contentType: PropTypes.string,
          buttonStyles: PropTypes.arrayOf(PropTypes.string),
          size: PropTypes.string,
          title: PropTypes.string,
          preText: PropTypes.string,
          postText: PropTypes.string,
        }),
      ),
    }),
  ),
};

Tabset.defaultProps = {
  tabStyles: ['default'],
  tabs: [{}],
};
