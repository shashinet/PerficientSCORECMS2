import React from 'react';
import PropTypes from 'prop-types';

// children
import ButtonLink from '../../ButtonLink';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function CtaCard(props) {
  const { block } = props;
  return (
    <>
      <div className={[...block.cardStyles].join(' ')}>
        <div className="caption">{block.heading && <h5>{block.heading}</h5>}</div>
        {block.callToAction && (
          <div className="call-to-action">
            {block.callToAction.map((btn) => (
              <ButtonLink
                title={btn.text}
                openInNewWindow={btn.openInNewWindow}
                url={btn.url}
                buttonStyles={[btn.style]}
              />
            ))}
          </div>
        )}
      </div>
    </>
  );
}

CtaCard.propTypes = {
  block: PropTypes.shape({
    cardStyles: PropTypes.arrayOf(PropTypes.string),
    heading: PropTypes.string,
    contentType: PropTypes.string,
    callToAction: PropTypes.arrayOf(PropTypes.shape({})),
  }),
};

CtaCard.defaultProps = {
  block: {
    cardStyles: [],
    heading: '',
    contentType: '',
    callToAction: [],
  },
};
