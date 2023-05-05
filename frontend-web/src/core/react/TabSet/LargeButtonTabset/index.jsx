import React from 'react';

// children
import LargeButtonTabDesktop from './Desktop';
import LargeButtonTabMobile from './Mobile';

// styles
import styles from './index.module.scss';

// hooks
import useWindowSize from '../../../hooks/useWindowSize';
import { largeButtonTabsetDefaultTypes, largeButtonTabsetTypes } from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function LargeButtonTabset(props) {
  const {
    title,
    titleTag,
    subTitleTag,
    subTitle,
    panels,
    globalStyle,
    callToActionButtons,
  } = props;

  const { width } = useWindowSize();

  return (
    <section className={[styles.largeButtonTabset, ...globalStyle].join(' ')}>
      <div className="container">
        <div className="w-full">
          {title && (
            <div className="section-header">
              {titleTag === 'H2' && (<h2>{title}</h2>)}
              {titleTag === 'H3' && (<h3>{title}</h3>)}
              {titleTag === 'H4' && (<h4>{title}</h4>)}
              {subTitle && (
                <div className="subtitle">
                  {subTitleTag === 'H2' && (<h2>{subTitle}</h2>)}
                  {subTitleTag === 'H3' && (<h3>{subTitle}</h3>)}
                  {subTitleTag === 'H4' && (<h4>{subTitle}</h4>)}
                </div>
              )}
            </div>
          )}
          {width > 992 ? (
            <>
              <LargeButtonTabDesktop
                callToActionButtons={callToActionButtons}
                panels={panels}
                subTitle={subTitle}
                subTitleTag={subTitleTag}
              />
            </>
          ) : (
            <LargeButtonTabMobile
              callToActionButtons={callToActionButtons}
              panels={panels}
              subTitle={subTitle}
              subTitleTag={subTitleTag}
            />
          )}
          {callToActionButtons && (
            <div className="cta-area">
              {callToActionButtons.map((btn) => (
                <>
                  <a
                    href={btn.url}
                    target={btn.openInNewWindow ? '__blank' : '__self'}
                    className={['score-buttonLink', btn.style].join(' ')}
                  >
                    {btn.text}
                  </a>
                </>
              ))}
            </div>
          )}
        </div>
      </div>
    </section>
  );
}
LargeButtonTabset.propTypes = {
  ...largeButtonTabsetTypes,
};

LargeButtonTabset.defaultProps = {
  ...largeButtonTabsetDefaultTypes,
};
