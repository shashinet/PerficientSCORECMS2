import React from 'react';

// styles
import styles from './index.module.scss';
import { largeButtonTabsetDesktopDefaultTypes, largeButtonTabsetDesktopTypes } from '../types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function LargeButtonTabDesktop(props) {
  const {
    panels,
  } = props;
  const openPanel = panels.length > 0 ? panels[0].buttonText : '';
  const [open, setOpen] = React.useState(openPanel);
  const keyEvent = (e) => {
    if (e.key === 'Enter') {
      setOpen(e.target.innerText);
    }
  };

  return (
    <div className={[styles.desktopButtonTabs, 'tabs'].join(' ')}>
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
                  {panel.callToActionButtons
                    && (
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
      <div className="tab-buttons-wrapper">
        {React.Children.toArray(panels.map((tabButton) => (
          <button
            type="button"
            id={`tab-${tabButton.buttonText.split(/\s/)
              .join('')}`}
            aria-controls={tabButton.buttonText.split(/\s/)
              .join('')}
            className={[open === tabButton.buttonText ? 'active' : 'out', 'tab-buttonLink'].join(' ')}
            data-id={tabButton.buttonText.split(/\s/)
              .join('')}
            onClick={() => setOpen(tabButton.buttonText)}
            onKeyUp={keyEvent}
          >
            {tabButton.buttonText}
          </button>
        )))}
      </div>
    </div>
  );
}

LargeButtonTabDesktop.propTypes = {
  ...largeButtonTabsetDesktopTypes,
};

LargeButtonTabDesktop.defaultProps = {
  ...largeButtonTabsetDesktopDefaultTypes,
};
