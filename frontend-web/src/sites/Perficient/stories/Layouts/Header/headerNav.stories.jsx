import React from 'react';
import Header from '../../../../../core/react/Header';
import data from './dataHeader.json';
import dataNoUtility from './dataNoUtility.json';

export default {
  title: 'Layouts/HeaderNav',
  component: Header,
  parameters: {
    layout: 'fullscreen',
    design: {
      type: 'figma',
      url: 'https://www.figma.com/file/yNiX9ZP9fXMaidWaMOjzDh/Design?node-id=3394%3A135911',
      allowfullscreen: true,
    },
  },
};

// eslint-disable-next-line react/jsx-props-no-spreading
const Template = (args) => <Header {...args} />;

export const HeaderNavigation = Template.bind({});

HeaderNavigation.args = {
  ...data,
};

export const HeaderNoUtilityWithTagline = Template.bind({});

HeaderNoUtilityWithTagline.args = {
  ...dataNoUtility,
};
