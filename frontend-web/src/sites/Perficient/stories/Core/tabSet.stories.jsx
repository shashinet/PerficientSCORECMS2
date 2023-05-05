import React from 'react';
import Tabsets from '../../../../core/react/TabSet/Default';
import data from './tabSet.json';

export default {
  title: 'Core/Tabset',
};
// eslint-disable-next-line react/jsx-props-no-spreading
const Template = (args) => <Tabsets {...args} />;

export const Tabs = Template.bind({});
Tabs.args = {
  ...data,
};
