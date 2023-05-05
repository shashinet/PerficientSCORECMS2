import React from 'react';
import ButtonLink from '../../../../core/react/ButtonLink';

export default {
  title: 'Core/Buttons',
  component: ButtonLink,
  argTypes: {
    buttonStyles: {
      description: 'Button class selections',
      default: 'primary',
    },
    size: {
      control: {
        type: 'inline-radio',
        options: ['sm', 'md', 'lg'],
      },
      description: 'Button size selections',
      default: 'md',
    },
  },
  parameters: {
    docs: {
      description: {
        component: '## Button is a component that can get a class and size and title passed in',
      },
      source: {
        code: ButtonLink,
      },
    },
  },
};

const Template = (args) => <ButtonLink {...args} />;
export const Primary = Template.bind({});
export const Secondary = Template.bind({});
export const PrimaryOverDark = Template.bind({});
export const SecondaryOverDark = Template.bind({});
export const PrimaryTextButton = Template.bind({});
export const PrimaryOverDarkTextButton = Template.bind({});
export const ButtonDisabled = Template.bind({});
export const TextButtonDisabled = Template.bind({});

Primary.args = {
  buttonStyles: ['primary'],
  size: 'md',
  title: 'Primary buttonLink',
};

Secondary.args = {
  buttonStyles: ['secondary'],
  size: 'md',
  title: 'Secondary Button',
};

PrimaryOverDark.args = {
  buttonStyles: ['primary', 'over-dark'],
  title: 'Primary buttonLink',
};

SecondaryOverDark.args = {
  buttonStyles: ['secondary', 'over-dark'],
  title: 'Secondary Button',
};

PrimaryTextButton.args = {
  buttonStyles: ['text-primary'],
  size: 'md',
  title: 'Read More',
};

PrimaryOverDarkTextButton.args = {
  buttonStyles: ['text-primary', 'over-dark'],
  title: 'Read More',
};

ButtonDisabled.args = {
  buttonStyles: ['disabled'],
  title: 'Primary Button',
};

TextButtonDisabled.args = {
  buttonStyles: ['disabled'],
  title: 'Read More',
};
