import React from 'react';
import PropTypes from 'prop-types';
import MobileTabPanel from '../../Panels';
import styles from './index.module.scss';

export default function VerticalTabMobile(props) {
  const {
    callToActionButtons,
    panels,
    subTitle,
    subTitleTag,
  } = props;

  return (
    <div className={[styles.mobileTabs, 'tabs'].join(' ')}>
      {subTitle && (
        <div className="subtitle">
          {subTitleTag === 'H2' && (<h2>{subTitle}</h2>)}
          {subTitleTag === 'H3' && (<h3>{subTitle}</h3>)}
          {subTitleTag === 'H4' && (<h4>{subTitle}</h4>)}
        </div>
      )}
      {React.Children.toArray(panels.map((item) => (
        <MobileTabPanel block={item} />
      )))}
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
  );
}

VerticalTabMobile.propTypes = {
  subTitle: PropTypes.string,
  subTitleTag: PropTypes.string,
  panels: PropTypes.arrayOf(
    PropTypes.shape({}),
  ),
  callToActionButtons: PropTypes.arrayOf(PropTypes.shape({})),
};

VerticalTabMobile.defaultProps = {
  subTitle: '',
  subTitleTag: '',
  panels: [],
  callToActionButtons: [],
};
