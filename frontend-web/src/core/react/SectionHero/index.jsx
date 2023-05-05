import React from 'react';
import types from './types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function SectionHeroBlock(props) {
  if (Object.keys(props).includes('block')) {
    // eslint-disable-next-line no-param-reassign, react/destructuring-assignment
    props = props.block;
  }

  const {
    sectionHeroStyles = ['default'],
    h2 = '',
    h3 = '',
    h4 = '',
    contentType = '',
    imageSrc = '',
    imageAlt = '',
    cta = '',
    body = '',
    videoSrc = '',
    videoThumb = '',
    videoTitle = '',
    videoType = '' } = props;

  const [load, setLoad] = React.useState(false);
  const video = React.useRef(null);

  const loadVideo = () => {
    setLoad(true);
    video.current.contentWindow.postMessage({
      event: 'command',
      func: 'playVideo',
    });
  };

  return (
    <>
      <section
        className={['section-hero-block', ...sectionHeroStyles, contentType === 'Image' ? '' : 'video'].join(' ')}
      >
        <div className="page-layout">
          {contentType && contentType === 'Image' ? (
            <img
              src={imageSrc}
              alt={imageAlt}
              className="score-image"
            />
          ) : (
            <div
              className={[`score-video ${videoType}`, load ? 'loaded' : ''].join(' ')}
              style={{ backgroundImage: !load ? videoThumb : '' }}
            >
              <button
                type="button"
                aria-label="video play button"
                onClick={loadVideo}
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="76"
                  height="76"
                  viewBox="0 0 76 76"
                  fill="none"
                >
                  <path
                    d="M30.5 54.875L53 38L30.5 21.125V54.875ZM38 0.5C17.3 0.5 0.5 17.3 0.5 38C0.5 58.7 17.3 75.5 38 75.5C58.7 75.5 75.5 58.7 75.5 38C75.5 17.3 58.7 0.5 38 0.5ZM38 68C21.4625 68 8 54.5375 8 38C8 21.4625 21.4625 8 38 8C54.5375 8 68 21.4625 68 38C68 54.5375 54.5375 68 38 68Z"
                    fill="white"
                  />
                </svg>
              </button>
              <iframe
                src={load ? `${videoSrc}?autoplay=1&enablejsapi=1` : ''}
                title={videoTitle}
                ref={video}
                allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
                allowFullScreen
              />
            </div>
          )}
        </div>
        <div className="page-layout">
          <div className="score-section-hero">
            {h2 && <h2>{h2}</h2>}
            {h3 && <h3>{h3}</h3>}
            {h4 && <h4>{h4}</h4>}
            {body && <div className="score-hero-body" dangerouslySetInnerHTML={{ __html: body }} />}
            {cta && <div className="score-call-to-action">{cta}</div>}
          </div>
        </div>
      </section>
    </>
  );
}

SectionHeroBlock.propTypes = {
  ...types,
};
