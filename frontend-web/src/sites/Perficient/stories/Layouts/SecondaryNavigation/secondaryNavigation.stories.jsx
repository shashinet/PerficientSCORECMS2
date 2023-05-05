import React from 'react';
import data from './data.json';
import SecondaryNavigation from '../../../../../core/react/SecondaryNavigation';

import mdx from './secondaryNavigation.mdx';

export default {
  title: 'Blocks/SecondaryNavigationBlock',
  component: SecondaryNavigation,
  parameters: {
    layout: 'fullscreen',
    docs: {
      page: mdx,
    },
  },
};

// eslint-disable-next-line react/jsx-props-no-spreading
const Template = (args) => <SecondaryNavigation {...args} />;

export const SecondaryNav = Template.bind({});

SecondaryNav.args = {
  ...data,
};

SecondaryNav.decorators = [
  (Story) => (
    <div className="flex-wrapper" style={{ height: '2500px', display: 'flex', flexDirection: 'row' }}>
      <Story />
    </div>
  ),
];
