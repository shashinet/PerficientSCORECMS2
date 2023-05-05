// @ts-check
import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function AccordionPanel(props) {
  const {
    title,
    panelContent,
  } = props;

  const [open, setOpen] = React.useState('');

  const keyEvent = (e) => {
    if (e.key === 'Enter') {
      setOpen(!open);
    }
  };

  return (
    <>
      <div className="score-accordion-panel">
        <div className="panel-heading">
          <div id="headingOne" className="panel-title">
            <button
              type="button"
              className={[open ? 'show' : '', 'accordion-button'].join(' ')}
              data-target="#accordion1"
              aria-expanded={open ? 'true' : 'false'}
              aria-controls="accordion1"
              onClick={() => setOpen(!open)}
              onKeyUp={keyEvent}
            >
              {title}
            </button>
          </div>
        </div>
        <div
          id="accordion1"
          className={open ? 'panel-collapse collapse show' : 'panel-collapse collapse'}
          aria-labelledby="headingOne"
          aria-hidden={open ? 'false' : 'true'}
        >
          <div className="panel-body">
            <div className="rich-text" dangerouslySetInnerHTML={{ __html: panelContent }} />
          </div>
        </div>
      </div>
    </>
  );
}

AccordionPanel.propTypes = {
  title: PropTypes.string,
  panelContent: PropTypes.string,
};

AccordionPanel.defaultProps = {
  title: '',
  panelContent: '',
};
