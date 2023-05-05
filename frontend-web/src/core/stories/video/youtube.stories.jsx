import React, {useEffect} from 'react';
import Stripe from '../../react/Stripe';
import Loader from '../../js/Video';

export default {
  title: 'Blocks/Video',
};

// Need to get data-src and create thumbnail
// Add click handler to remove image and switch data-src to src

export function YouTubeBlock() {
  useEffect(() => {
    Loader.init();
  }, []);

  return (
    <>
      <Stripe selections="section-block">
        <div className="w-full">
          <div className="content-area">
            <div className="w-6col">
              <div className="youtube score-video score-youtube-video"
                   style={{backgroundImage: 'http://img.youtube.com/vi/-ox5Y9jJEC0/maxresdefault.jpg'}}>
                <iframe
                  data-src="https://www.youtube.com/embed/-ox5Y9jJEC0"
                  data-thumbnailurl="http://img.youtube.com/vi/-ox5Y9jJEC0/maxresdefault.jpg"
                  frameBorder="0"
                  allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
                  allowFullScreen={true}
                />
              </div>
            </div>
            <div className="w-6col"></div>
          </div>
        </div>
      </Stripe>
    </>
  );
}
