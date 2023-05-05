import React from 'react';
import ButtonLink from '../../react/ButtonLink';

export default {
  title: 'Components/Button',
  component: ButtonLink,
  argTypes: {
    buttonStyles: {
      description: 'Button class selections',
      default: 'primary'
    },
    size: {
      control: {
        type: 'inline-radio',
        options: ['sm', 'md', 'lg']
      },
      description: 'Button size selections',
      default: 'md'
    }
  },
  parameters: {
    docs: {
      description: {
        component:
          '## Button is a component that can get a class and size and title passed in'
      },
      source: {
        code: ButtonLink
      }
    }
  }
};

const Template = (args) => <ButtonLink {...args} />;

export const PrimaryButton = Template.bind({});
export const SecondaryButton = Template.bind({});

PrimaryButton.args = {
  buttonStyles: ['primary'],
  size: 'md',
  title: 'Read More'
};

SecondaryButton.args = {
  buttonStyles: ['secondary'],
  size: 'md',
  title: 'Read More'
};
