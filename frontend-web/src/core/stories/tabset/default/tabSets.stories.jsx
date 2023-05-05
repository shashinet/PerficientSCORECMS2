import React from 'react';
import Tabsets from '../../../react/TabSet/Default';
import Card from '../../../react/Card';
import ButtonLink from '../../../react/ButtonLink';
import './tabset.scss';

export default {
  title: 'Components/Tabset',
};

// eslint-disable-next-line react/jsx-props-no-spreading
export const Tabs = args => <Tabsets {...args} />;

Tabs.args = {
  tabs: [
    {
      label: 'label1',
      innerContent: (
        <Card
          h2="Test area content Tab One"
          body="lorem impum something upum"
          cta={<ButtonLink buttonStyles="primary" label="Read More"/>}
        />
      ),
    },

    {
      label: 'label2',
      innerContent: (
        <Card
          h2="Test area content Tab Two"
          body="lorem impum something upum"
          cta={<ButtonLink buttonStyles="primary" label="Read More"/>}
        />
      ),
    },
    {
      label: 'label3',
      innerContent: (
        <Card
          h2="Test area content Tab Three"
          body="lorem impum something upum"
          cta={<ButtonLink buttonStyles="primary" label="Read More"/>}
        />
      ),
    },
  ],
};

Tabs.decorators = [
  (Story) => (
    <div className={'container w-8col'}>
      <Story/>
    </div>
  )
];
