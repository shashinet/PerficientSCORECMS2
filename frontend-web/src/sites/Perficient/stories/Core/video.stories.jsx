/* eslint-disable jsx-a11y/iframe-has-title */
import React, {useEffect} from 'react';
import Stripe from '../../../../core/react/Stripe';
import Loader from '../../../../core/js/Video';

export default {
  title: 'Core/Video',
};

// Need to get data-src and create thumbnail
// Add click handler to remove image and switch data-src to src

export function YouTubeBlock() {
  useEffect(() => {
    Loader.init();
  }, []);

  return (
    <>
      <Stripe>
        <div className="w-6col">
          <div
            className="youtube score-video score-youtube-video"
            style={{backgroundImage: 'http://img.youtube.com/vi/EngW7tLk6R8/maxresdefault.jpg'}}
          >
            <iframe
              data-src="https://www.youtube.com/embed/EngW7tLk6R8"
              data-thumbnailurl="http://img.youtube.com/vi/EngW7tLk6R8/maxresdefault.jpg"
              frameBorder="0"
              allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
              allowFullScreen
            />
          </div>
        </div>
        <div className="w-6col"/>
      </Stripe>
    </>
  );
}

export function VimeoBlock() {
  useEffect(() => {
    Loader.init();
  }, []);

  return (
    <>
      <Stripe>
        <div className="w-6col">
          <div
            className="vimeo score-video score-vimeo-video"
            style={{backgroundImage: 'https://i.vimeocdn.com/video/506187830'}}
          >
            <iframe
              data-src="https://player.vimeo.com/video/33698814"
              data-thumbnailurl="https://i.vimeocdn.com/video/506187830"
              frameBorder="0"
              allow="autoplay; encrypted-media"
              allowFullScreen
            />
          </div>
        </div>
        <div className="w-6col"/>
      </Stripe>
    </>
  );
}
