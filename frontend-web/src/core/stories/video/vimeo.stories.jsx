import React, {useEffect} from 'react';
import Stripe from '../../react/Stripe';
import Loader from '../../js/Video';

export default {
  title: 'Blocks/Video',
};

// Need to get data-src and create thumbnail
// Add click handler to remove image and switch data-src to src

export function VimeoBlock() {
  useEffect(() => {
    Loader.init();
  }, []);

  return (
    <>
      <Stripe selections="section-block">
        <div className="w-6col">
          <div
            className="vimeo score-video score-vimeo-video"
            style={{backgroundImage: 'https://i.vimeocdn.com/video/506187830'}}
          >
            <iframe
              data-src="https://player.vimeo.com/video/33698814"
              data-thumbnailurl="https://i.vimeocdn.com/video/506187830"
              frameborder="0"
              allow="autoplay; encrypted-media"
              allowFullScreen={true}
            />
          </div>
        </div>
      </Stripe>
    </>
  );
}
