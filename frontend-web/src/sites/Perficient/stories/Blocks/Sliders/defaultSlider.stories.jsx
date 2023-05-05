import React from 'react';
import data from './defaultSlider.json';
import DefaultSlider from '../../../../../core/react/Sliders/DefaultSlider/defaultSlider';
import mdx from './defaultSlider.mdx';

export default {
  title: 'Blocks/Sliders',
  component: DefaultSlider,
  parameters: {
    layout: 'fullscreen',
    docs: {
      mdx,
    },
    design: {
      type: 'figma',
      url: 'https://www.figma.com/file/yNiX9ZP9fXMaidWaMOjzDh/Design?node-id=3212%3A108604',
      allowfullscreen: true,
    },
  },
};

// eslint-disable-next-line react/jsx-props-no-spreading
const Template = (args) => <DefaultSlider {...args} />;

export const DefaultSliderBlock = Template.bind({});
DefaultSliderBlock.args = {
  ...data,
};
