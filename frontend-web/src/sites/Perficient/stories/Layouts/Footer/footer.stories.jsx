import React from 'react';
import MainFooter from '../../../../../core/react/Footer';
import data from './footer.json';
import dataBasic from './footerData.json';

export default {
  title: 'Layouts/Footer',
  component: MainFooter,
  argTypes: {
    footerStyle: {
      control: {
        type: 'inline-radio',
        options: ['dark', 'light'],
      },
      description: 'Footer varients',
      default: 'dark',
    },
    globalStyle: {
      control: {
        type: 'inline-radio',
        options: ['mt-8', 'mt-2', 'mt-0'],
      },
      description: 'Footer global selections',
      default: 'mt-0',
    },
  },
  parameters: {
    layout: 'fullscreen',
    design: {
      type: 'figma',
      url: 'https://www.figma.com/file/yNiX9ZP9fXMaidWaMOjzDh/Design?node-id=3394%3A135338',
      allowfullscreen: true,
    },
  },
};
// eslint-disable-next-line react/jsx-props-no-spreading
const Template = (args) => <MainFooter {...args} />;

export const FooterNavigation = Template.bind({});
FooterNavigation.args = {
  ...data,
};

export const BasicFooter = Template.bind({});
BasicFooter.args = {
  ...dataBasic,
};
