import React from 'react';
import Hero from '../../../../core/react/Hero';
import ButtonLink from '../../../../core/react/ButtonLink';

export default {
  title: 'Core/Hero',
  component: Hero,
  argTypes: {
    backgroundColor: {
      control: {
        type: 'color',
      },
    },
    backgroundImage: {
      control: {
        type: 'file',
      },
    },
    imageSrc: {
      control: {
        type: 'file',
      },
    },
    body: {
      control: {
        type: 'text',
      },
    },
    cta: {
      control: {
        type: 'text',
      },
    },
    color: {
      control: {
        type: 'color',
      },
    },
    selections: {
      control: {
        type: 'inline-radio',
        options: ['default'],
      },
      description: 'Hero class selections',
      default: 'default',
    },
  },
  parameters: {
    docs: {
      description: {
        component:
          '## Hero is a heading with h1, h2, image, body(rte), cta that can have a color, backgroundColor, backgroundImage, or style selection passed in',
      },
      source: {
        code: Hero,
      },
    },
  },
};

const Template = (args) => <Hero {...args} />;

export const Default = Template.bind({});

Default.args = {
  color: '#000',
  backgroundColor: '#fff',
  backgroundImage: '',
  heroStyles: ['default'],
  h1: 'This is a title',
  imageSrc: 'https://via.placeholder.com/1920x1080',
  body: '<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries</p>',
  cta: <ButtonLink selections={['primary']} title={'Read More'}></ButtonLink>,
};
