import React from 'react';
import SectionHeader from '../../../../core/react/SectionHeader';

export default {
  title: 'Core/Section Header',
  component: SectionHeader,
  argTypes: {
    h1: { control: { type: 'text' } },
    h2: { control: { type: 'text' } },
    imageSrc: { control: { type: 'file' } },
    selections: {
      control: {
        type: 'inline-radio',
        options: ['default'],
      },
      description: 'SectionHeader class selections',
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
        code: SectionHeader,
      },
    },
  },
};

const Template = (args) => <SectionHeader {...args} />;

export const Default = Template.bind({});

Default.args = {
  h2: 'h2',
  h3: 'h3',
  imageSrc: 'http://placehold.it/300x200&text=[doc header img]',
  sectionHeaderStyles: ['default'],
};
