import React from 'react';

// types
import { stripeDefaultTypes, stripeTypes } from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function Stripe(props) {
  const {
    backgroundColor,
    backgroundImage,
    children,
    color,
    headingAlign,
    ctaAlign,
    subtitle,
    heading2,
    heading3,
    heading4,
    stripeStyles,
    alignment,
    cta,
  } = props;
  return (
    <section
      className={['score-stripe', ...stripeStyles].join(' ')}
      style={{
        backgroundImage: `${backgroundImage} ? url(${backgroundImage}): '`,
        backgroundColor,
        color,
      }}
    >
      <div className={['container', `${alignment}`].join(' ')}>
        <div className="w-full">
          {heading2 || heading3 || heading4 ? (
            <div className={['section-header', `${headingAlign}`].join(' ')}>
              {heading2 && <h2>{heading2}</h2>}
              {heading3 && <h3>{heading3}</h3>}
              {heading4 && <h4>{heading4}</h4>}
              {subtitle && <p className="subtitle">{subtitle}</p>}
            </div>
          ) : null}
          {children && <div className={['content-area', `${alignment}`].join(' ')}>{children}</div>}
          {cta && <div className={['cta', `${ctaAlign}`].join(' ')}>{cta}</div>}
        </div>
      </div>
    </section>
  );
}

Stripe.propTypes = {
  ...stripeTypes,
};

Stripe.defaultProps = {
  ...stripeDefaultTypes,
};
