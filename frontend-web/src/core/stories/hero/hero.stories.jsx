import React from 'react';
import Hero from '../../react/Hero';
import './hero.scss';
import { PrimaryButton } from '../buttonLink/buttonLink.stories';

export default {
  title: 'Components/Hero',
  component: Hero,
  argTypes: {
    backgroundColor: {
      control: {
        type: 'color'
      }
    },
    backgroundImage: {
      control: {
        type: 'file'
      }
    },
    imageSrc: {
      control: {
        type: 'file'
      }
    },
    color: {
      control: {
        type: 'color'
      }
    }
  },
  parameters: {
    docs: {
      description: {
        component:
          '## Hero is a heading with h1, h2, image, body(rte), cta that can have a color, backgroundColor, backgroundImage, or style heroStyles passed in'
      },
      source: {
        code: Hero
      }
    }
  }
};

const Template = (args) => <Hero {...args} />;

export const DefaultHeroBackgroundImage = Template.bind({});

DefaultHeroBackgroundImage.args = {
  color: '#000',
  backgroundColor: '#fff',
  backgroundImage: 'https://picsum.photos/1900/500',
  heroStyles: ['default', 'h-[500px] w-[500px]'],
  h1: 'h1 Heading',
  h2: 'h2 Heading(Subtitle)',
  body: '<div><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p></div>',
  cta: <PrimaryButton {...PrimaryButton.args} />
};
