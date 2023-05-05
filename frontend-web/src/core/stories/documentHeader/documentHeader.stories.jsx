import React from 'react';
import DocumentHeader from '../../react/DocumentHeader';
import './document-header.scss';

export default {
  title: 'Components/DocumentHeader',
  component: DocumentHeader,
  argTypes: {
    h1: { control: { type: 'text' } },
    h2: { control: { type: 'text' } },
    imageSrc: { control: { type: 'file' } },
    documentHeaderStyles: {
      control: {
        type: 'inline-radio',
        options: ['default', 'lg', 'sm'],
      },
      description: 'DocumentHeader class documentHeaderStyles',
      default: 'default',
    },
  },
  parameters: {
    docs: {
      description: {
        component:
          '## Document Header is a header with a h1, h2, and image element',
      },
      source: {
        code: DocumentHeader,
      },
    },
  },
};

const Template = (args) => <DocumentHeader {...args} />;

export const Default = Template.bind({});
export const DefaultCenter = Template.bind({});

Default.args = {
  h1: 'h1 Header',
  h2: 'h2 Header/Subheader',
  imageSrc: 'https://picsum.photos/1900/500',
  documentHeaderStyles: ['default'],
};

DefaultCenter.args = {
  h1: 'h1 Header',
  h2: 'h2 Header/Subheader',
  imageSrc: 'https://picsum.photos/1900/500',
  documentHeaderStyles: ['center'],
};
