import React from 'react';

// styles
import styles from './index.module.scss';

// types
import { verticalTabsetDesktopDefaultTypes, verticalTabsetDesktopTypes } from '../types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function VerticalTabDesktop(props) {
  const {
    callToActionButtons,
    panels,
    subTitle,
    subTitleTag,
  } = props;
  const [open, setOpen] = React.useState(panels[0].buttonText);
  const keyEvent = (e) => {
    if (e.key === 'Enter') {
      setOpen(e.target.innerText);
    }
  };

  return (
    <div className={[styles.desktopTabs, 'tabs'].join(' ')}>
      <div className="tab-buttons-wrapper">
        {subTitle && (
          <div className="subtitle">
            {subTitleTag === 'H2' && (<h2>{subTitle}</h2>)}
            {subTitleTag === 'H3' && (<h3>{subTitle}</h3>)}
            {subTitleTag === 'H4' && (<h4>{subTitle}</h4>)}
          </div>
        )}
        {React.Children.toArray(panels.map((tabButton) => (
          <button
            type="button"
            id={`tab-${tabButton.buttonText.split(/\s/)
              .join('')}`}
            aria-controls={tabButton.buttonText.split(/\s/)
              .join('')}
            className={[open === tabButton.buttonText ? 'active' : 'out', 'tab-button'].join(' ')}
            data-id={tabButton.buttonText.split(/\s/)
              .join('')}
            onClick={() => setOpen(tabButton.buttonText)}
            onKeyUp={keyEvent}
          >
            {tabButton.buttonText}
          </button>
        )))}
        {callToActionButtons && (
          <div className="cta-area">
            {callToActionButtons.map((btn) => (
              <>
                <a
                  href={btn.url}
                  target={btn.openInNewWindow ? '__blank' : '__self'}
                  className={['score-button', btn.style].join(' ')}
                >
                  {btn.text}
                </a>
              </>
            ))}
          </div>
        )}
      </div>
      <div className="tab-content">
        <>
          {React.Children.toArray(panels.map((panel) => (
            <div
              className={[open === panel.buttonText ? 'active' : 'fade-out', 'content'].join(' ')}
              id={panel.buttonText.split(/\s/)
                .join('')}
              aria-labelledby={`tab-${panel.buttonText.split(/\s/)
                .join('')}`}
            >
              <div className="panel">
                <>
                  {(() => {
                    switch (panel.image.contentType) {
                      case 'ResponsiveImage':
                        return (
                          <div className="image-wrapper">
                            <picture>
                              {React.Children.toArray(panel.image.croppings
                                && panel.image.croppings.map((src) => (
                                  <source srcSet={src.imageSrc} media={src.srcSet} />
                                )))}
                              {panel.image.original && (
                                <img
                                  src={panel.image.original.imageSrc}
                                  alt={panel.image.original.altText}
                                />
                              )}
                            </picture>
                          </div>
                        );
                      case 'Image':
                        return (
                          <div className="image-wrapper">
                            <img src={panel.image.imageSrc} alt={panel.image.altText} />
                          </div>
                        );
                      default:
                        return (
                          <div
                            className="image-wrapper"
                            aria-label={panel.image.altText}
                            title={panel.image.altText}
                            dangerouslySetInnerHTML={{ __html: panel.image.imageSrc }}
                          />
                        );
                    }
                  })()}
                </>
                <div className="caption">
                  {panel.title && (
                    <div className="title">
                      <span>{panel.title}</span>
                    </div>
                  )}
                  {panel.text && (
                    <div className="body" dangerouslySetInnerHTML={{ __html: panel.text }} />
                  )}
                  {panel.callToActionButtons && (
                    <div className="cta-area">
                      {React.Children.toArray(panel.callToActionButtons.map((btn) => (
                        <a
                          href={btn.url}
                          target={btn.openInNewWindow ? '__blank' : '__self'}
                          className={['score-buttonLink', btn.style].join(' ')}
                        >
                          {btn.text}
                        </a>
                      )))}
                    </div>
                  )}
                </div>
              </div>
            </div>
          )))}
        </>
      </div>
    </div>
  );
}

VerticalTabDesktop.propTypes = {
  ...verticalTabsetDesktopTypes,
};

VerticalTabDesktop.defaultProps = {
  ...verticalTabsetDesktopDefaultTypes,
};
