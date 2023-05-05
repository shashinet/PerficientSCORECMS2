import React from 'react';

// children
import VerticalTabDesktop from './Desktop';
import VerticalTabMobile from './Mobile';

// styles
import styles from './index.module.scss';

// hooks
import useWindowSize from '../../../hooks/useWindowSize';

// types
import { verticalTabsetDefaultTypes, verticalTabsetTypes } from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function VerticalTabset(props) {
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
    <section className={[styles.verticalTabset, ...globalStyle].join(' ')}>
      <div className="container">
        <div className="w-full">
          {title && (
            <div className="section-header">
              {titleTag === 'H2' && (<h2>{title}</h2>)}
              {titleTag === 'H3' && (<h3>{title}</h3>)}
              {titleTag === 'H4' && (<h4>{title}</h4>)}
            </div>
          )}
          {width > 992 ? (
            <>
              <VerticalTabDesktop
                callToActionButtons={callToActionButtons}
                panels={panels}
                subTitle={subTitle}
                subTitleTag={subTitleTag}
              />
            </>
          ) : (
            <VerticalTabMobile
              callToActionButtons={callToActionButtons}
              panels={panels}
              subTitle={subTitle}
              subTitleTag={subTitleTag}
            />
          )}
        </div>
      </div>
    </section>
  );
}
VerticalTabset.propTypes = {
  ...verticalTabsetTypes,
};

VerticalTabset.defaultProps = {
  ...verticalTabsetDefaultTypes,
};
