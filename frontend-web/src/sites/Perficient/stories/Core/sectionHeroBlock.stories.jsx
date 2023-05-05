import React from 'react';
import SectionHero from '../../../../core/react/SectionHero';
import ButtonLink from '../../../../core/react/ButtonLink';

export default {
  title: 'Core/SectionHero',
  parameters: {
    layout: 'fullscreen',
    design: {
      allowfullscreen: true,
    },
  },
};

const Template = (args) => <SectionHero {...args} />;

export const SectionHeroImage = Template.bind({});
export const SectionHeroImageRight = Template.bind({});
export const SectionHeroVideo = Template.bind({});

SectionHeroImage.args = {
  imageSrc: 'https://picsum.photos/500/300',
  imageAlt: 'Alt text goes herea',
  contentType: 'Image',
  h2: 'Urgent Care Brand',
  body: '<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>',
  cta: <ButtonLink buttonStyles={['primary']} title="Find Urgent Care" url="www.google.com"/>,
};

SectionHeroImage.decorators = [
  (Story) => (
    <section className={'score-stripe default'}>
      <div className="container">
        <div className="w-full">
          <Story/>
        </div>
      </div>
    </section>
  )
];

SectionHeroImageRight.args = {
  imageSrc: 'https://picsum.photos/500/300',
  sectionHeroStyles: ['right'],
  imageAlt: 'Alt text goes herea',
  contentType: 'Image',
  h2: 'Urgent Care Brand',
  body: '<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>',
  cta: <ButtonLink buttonStyles={['primary']} title="Find Urgent Care" url="www.google.com"/>,
};

SectionHeroImageRight.decorators = [
  (Story) => (
    <section className={'score-stripe default'}>
      <div className="container">
        <div className="w-full">
          <Story/>
        </div>
      </div>
    </section>
  )
];

SectionHeroVideo.args = {
  videoSrc: 'https://www.youtube.com/embed/RK1K2bCg4J8',
  videoThumb: 'https://i.ytimg.com/vi_webp/RK1K2bCg4J8/sddefault.webp',
  videoTitle: 'test vidzs',
  videoType: 'youtube',
  sectionHeroStyles: ['right'],
  h2: 'Virtual Care Brand Message',
  body: '<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p> <span class="eyebrow">Virtural Care Stats</span><ul><li>Lorem ipsum dolor sitsum</li><li>Lorem ipsum dolor sitsum</li><li>Lorem ipsum dolor sitsum</li></ul>',
  cta: [<ButtonLink buttonStyles={['primary']} title="Find Urgent Care" url="www.google.com"/>,
    <ButtonLink buttonStyles={['primary']} title="Find Urgent Care" url="www.google.com"/>],
};

SectionHeroVideo.decorators = [
  (Story) => (
    <section className={'score-stripe default bg-lightgrey'}>
      <div className="container">
        <div className="w-full">
          <Story/>
        </div>
      </div>
    </section>
  )
];
