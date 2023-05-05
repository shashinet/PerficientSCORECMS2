import React from 'react';
import VerticalTabset from '../../../../../core/react/TabSet/VerticalTabset';
import data from './verticalTabset.json';

export default {
  title: 'Blocks/VerticalTabset',
  component: VerticalTabset,
  parameters: {
    layout: 'fullscreen',
  },
};

const Template = (args) => <VerticalTabset {...args} />;
export const VerticalTabBlock = Template.bind({});

VerticalTabBlock.args = {
  ...data,
};
