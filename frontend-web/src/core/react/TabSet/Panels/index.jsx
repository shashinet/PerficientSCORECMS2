import React from 'react';

// types
import { mobileTabPanelDefaultTypes, mobileTabPanelTypes } from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function MobileTabPanel(props) {
  const {
    block,
  } = props;

  const [open, setOpen] = React.useState('');
  const keyEvent = (e) => {
    if (e.key === 'Enter') {
      setOpen(!open);
    }
  };

  return (
    <>
      <div className="tab-panel">
        <div className="panel-heading">
          <div id={block.buttonText} className="panel-title">
            <button
              type="button"
              className={[open ? 'show' : '', 'tab-buttonLink'].join(' ')}
              data-target="#accordion1"
              aria-expanded={open ? 'true' : 'false'}
              aria-controls="accordion1"
              onClick={() => setOpen(!open)}
              onKeyUp={keyEvent}
              data-id={block.buttonText}
            >
              {block.buttonText}
            </button>
          </div>
        </div>
        <div
          id={block.buttonText}
          className={open ? 'panel-collapse collapse show' : 'panel-collapse collapse'}
          aria-labelledby={block.buttonText}
          aria-hidden={open ? 'false' : 'true'}
        >
          <div className="panel-body">
            <>
              {(() => {
                switch (block.image.contentType) {
                  case 'ResponsiveImage':
                    return (
                      <div className="image-wrapper">
                        <picture>
                          {React.Children.toArray(block.image.croppings
                            && block.image.croppings.map((src) => (
                              <source srcSet={src.imageSrc} media={src.srcSet} />
                            )))}
                          {block.image.original && (
                            <img
                              src={block.image.original.imageSrc}
                              alt={block.image.original.altText}
                            />
                          )}
                        </picture>
                      </div>
                    );
                  case 'Image':
                    return (
                      <div className="image-wrapper">
                        <img src={block.image.imageSrc} alt={block.image.altText} />
                      </div>
                    );
                  default:
                    return (
                      <div
                        className="image-wrapper"
                        aria-label={block.image.altText}
                        title={block.image.altText}
                        dangerouslySetInnerHTML={{ __html: block.image.imageSrc }}
                      />
                    );
                }
              })()}
            </>
            {block.title && (
              <div className="title">
                <span>{block.title}</span>
              </div>
            )}
            {block.text && (
              <div className="body" dangerouslySetInnerHTML={{ __html: block.text }} />
            )}
            {block.callToActionButtons && (
              <div className="cta-area">
                {React.Children.toArray(block.callToActionButtons.map((btn) => (
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
    </>
  );
}

MobileTabPanel.propTypes = {
  ...mobileTabPanelTypes,
};

MobileTabPanel.defaultProps = {
  ...mobileTabPanelDefaultTypes,
};
