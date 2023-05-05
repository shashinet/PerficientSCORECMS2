import React from 'react';
import Video from '../../../../core/react/Video/video';

export default {
  title: 'Components/Video',
};

export function LargeVideoBlock() {
  return (
    <>
      <section className="video-block">
        <div className="container">
          <div className="w-full">
            <div className="section-header">
              <h2>Informative and Clarifying Video Headline</h2>
            </div>
            <Video
              videoStyles={['large']}
              videoSrc="https://www.youtube.com/embed/RK1K2bCg4J8"
              videoThumb="https://i.ytimg.com/vi_webp/RK1K2bCg4J8/sddefault.webp"
              videoTitle="video"
              contentType="video"
              videoType="youtube"
            />
            <p className="description">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. <a
              href="dev/src/Web/wwwroot/src/sites/Perficient/stories/Components/video.stories#">View Transcript</a>
            </p>
          </div>
        </div>
      </section>
    </>
  );
}

LargeVideoBlock.decorators = [
  (Story) => (
    <div className="container" style={{margin: '0 auto', width: '100%', height: '555px'}}>
      <Story/>
    </div>
  ),
];
