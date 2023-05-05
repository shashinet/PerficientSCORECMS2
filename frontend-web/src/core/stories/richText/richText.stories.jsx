import React from 'react';
import RichText from '../../react/RichText/richText';
import './rich-text.scss';

export default {
  title: 'Components/RichText',
  component: RichText,
  argTypes: {
    contentSpotStyles: {
      control: {
        type: 'inline-radio',
        options: ['center', 'left-aligned', 'default'],
      },
      description: 'TextPanel class selections',
      default: 'primary',
    },
    children: { control: { type: 'text' } },
  },
  parameters: {
    docs: {
      description: {
        component:
          '## RichText is a component can have different variations by a CA adding class selections to the component',
      },
      source: {
        code: RichText,
      },
    },
  },
};

const Template = (args) => <RichText {...args} />;

export const Default = Template.bind({});
export const Accent = Template.bind({});

Default.args = {
  contentSpotStyles: ['default'],
  children:
    '<h2>Heading h2</h2><h3>Heading h3</h3><h4>Heaing h4</h4><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor <a href="/">incididunt</a> ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p>',
};

Default.decorators = [
  (Story) => (
    <div style={{ maxWidth: '50%' }}>
      <Story />
    </div>
  ),
];
Accent.args = {
  contentSpotStyles: ['accent'],
  children:
    '<h2>Heading h2</h2><h3>Heading h3</h3><h4>Heaing h4</h4><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor <a href="/">incididunt</a> ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p>',
};

Accent.decorators = [
  (Story) => (
    <div style={{ maxWidth: '50%' }}>
      <Story />
    </div>
  ),
];
