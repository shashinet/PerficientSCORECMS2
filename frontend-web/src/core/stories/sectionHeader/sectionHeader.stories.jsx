import React from 'react';
import SectionHeader from '../../react/SectionHeader';
import './section-header.scss';

export default {
  title: 'Components/SectionHeader',
  component: SectionHeader,
  argTypes: {
    h1: { control: { type: 'text' } },
    h2: { control: { type: 'text' } },
    imageSrc: { control: { type: 'file' } },
  },
  parameters: {
    docs: {
      description: {
        component:
          '## Document Header is a header with a h2, h3, and image element',
      },
      source: {
        code: SectionHeader,
      },
    },
  },
};

const Template = (args) => <SectionHeader {...args} />;

export const Default = Template.bind({});
export const NoImageCenter = Template.bind({});

Default.args = {
  h2: 'Section Heading Text',
  h3: 'Section Subheading Text',
  imageSrc: 'https://picsum.photos/1900/500',
  sectionHeaderStyles: ['default'],
};

NoImageCenter.args = {
  h2: 'Section Heading Text Center ',
  h3: 'Section Subheading Text Center',
  sectionHeaderStyles: ['center'],
};
