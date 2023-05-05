/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import DocumentHeader from '../../../../core/react/DocumentHeader';

export default {
  title: 'Core/Document Header',
  component: DocumentHeader,
  argTypes: {
    h1: { control: { type: 'text' } },
    h2: { control: { type: 'text' } },
    imageSrc: { control: { type: 'file' } },
    selections: {
      control: {
        type: 'inline-radio',
        options: ['default'],
      },
      description: 'DocumentHeader class selections',
      default: 'default',
    },
  },
  parameters: {
    docs: {
      description: {
        component:
          '## Document Header is a header with a h1, h2(subtitle), and image element',
      },
      source: {
        code: DocumentHeader,
      },
    },
  },
};

const Template = (args) => (
  <>
    <DocumentHeader {...args} />
    <div>I AM HERE</div>
  </>
);

export const Default = Template.bind({});
export const Dark = Template.bind({});

Default.args = {
  h1: 'Here is the main title',
  h2: 'subtitle',
  imageSrc: 'https://picsum.photos/1600/400',
  documentHeaderStyles: ['default'],
};

Dark.args = {
  h1: 'Here is the main title',
  h2: 'subtitle',
  imageSrc: 'https://picsum.photos/1600/400',
  documentHeaderStyles: ['dark'],
};
