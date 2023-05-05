/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import PricingCard from '../../../../core/react/PricingCard';
import { Primary, Secondary } from './buttons.stories';

export default {
  title: 'Core/Pricing Card',
  component: PricingCard,
  argTypes: {
    imageSrc: {
      control: {
        type: 'file',
      },
    },
  },
  parameters: {
    docs: {
      description: {
        component:
          '## The Pricing Card is a component with heading, subheading, image, price, price term, body(rte), and a cta that can have style selection passed in',
      },
      source: {
        code: PricingCard,
      },
    },
  },
};

const Template = (args) => <PricingCard {...args} />;

export const DefaultPricingCard = Template.bind({});
export const SelectedPricingCard = Template.bind({});

DefaultPricingCard.args = {
  cardSyles: ['default'],
  h2: 'h2 title',
  h3: 'h2 title',
  price: '$10.99',
  priceTerm: 'Monthly',
  imageSrc: 'https://picsum.photos/300/200',
  body: '<ul><li><b>10GB</b> Disk Space</li><li><b>100GB</b> Monthly Bandwidth</li><li><b>20</b> Email Accounts</li><li><b>Unlimited</b> subdomains</li></ul>',
  cta: <Primary {...Primary.args} />,
};

DefaultPricingCard.decorators = [
  (Story) => (
    <div style={{ maxWidth: '20rem' }}>
      <Story />
    </div>
  ),
];

SelectedPricingCard.args = {
  cardSyles: ['selected'],
  h2: 'h2 title',
  h3: 'h2 title',
  imageSrc: 'https://picsum.photos/300/200',
  body: '<ul><li><b>10GB</b> Disk Space</li><li><b>100GB</b> Monthly Bandwidth</li><li><b>20</b> Email Accounts</li><li><b>Unlimited</b> subdomains</li></ul>',
  cta: <Secondary {...Secondary.args} />,
};

SelectedPricingCard.decorators = [
  (Story) => (
    <div style={{ maxWidth: '20rem' }}>
      <Story />
    </div>
  ),
];
