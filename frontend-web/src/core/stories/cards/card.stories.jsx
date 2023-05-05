/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import Card from '../../react/Card';
import { PrimaryButton, SecondaryButton } from '../buttonLink/buttonLink.stories';
import './card.scss';

export default {
  title: 'Components/Card',
  component: Card,
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
          '## Card is a component with heading, h2, h3, h4, image, body(rte), cta that can have style selection passed in',
      },
      source: {
        code: Card,
      },
    },
  },
};

const Template = (args) => <Card {...args} />;

export const DefaultCard = Template.bind({});
export const DefaultCardCenter = Template.bind({});
export const PrimaryCard = Template.bind({});
export const PrimaryCardHorizontal = Template.bind({});
export const PrimaryCardNoImage = Template.bind({});
export const PrimaryCardNoCta = Template.bind({});
export const SecondaryCard = Template.bind({});

DefaultCard.args = {
  cardSyles: ['primary', 'p-5 flex flex-col'],
  h2: 'h2 title',
  imageSrc: 'https://picsum.photos/300/200',
  body: '<div><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor libero id</p></div>',
  cta: <PrimaryButton {...PrimaryButton.args} />,
};

DefaultCard.decorators = [
  (Story) => (
    <div style={{ maxWidth: '20rem' }}>
      <Story />
    </div>
  ),
];

DefaultCardCenter.args = {
  cardSyles: ['primary'],
  h2: 'h2 title',
  imageSrc: 'https://picsum.photos/200/100',
  body: '<div><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor libero id</p></div>',
  cta: <PrimaryButton {...PrimaryButton.args} />,
};

DefaultCardCenter.decorators = [
  (Story) => (
    <div style={{ maxWidth: '20rem' }}>
      <Story />
    </div>
  ),
];

PrimaryCard.args = {
  cardSyles: ['primary'],
  h2: 'h2 title',
  imageSrc: 'https://picsum.photos/300/300',
  body: '<div><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p></div>',
  cta: <PrimaryButton {...PrimaryButton.args} />,
};

PrimaryCard.decorators = [
  (Story) => (
    <div style={{ maxWidth: '20rem' }}>
      <Story />
    </div>
  ),
];

PrimaryCardHorizontal.args = {
  cardSyles: ['primary', 'horizontal'],
  h2: 'h2 title',
  imageSrc: 'https://picsum.photos/300/300',
  body: '<div><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p></div>',
  cta: <PrimaryButton {...PrimaryButton.args} />,
};

PrimaryCardHorizontal.decorators = [
  (Story) => (
    <div style={{ maxWidth: '40rem' }}>
      <Story />
    </div>
  ),
];

PrimaryCardNoImage.args = {
  cardSyles: ['primary'],
  h3: 'Title',
  body: '<div><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p></div>',
  cta: <PrimaryButton {...PrimaryButton.args} />,
};

PrimaryCardNoImage.decorators = [
  (Story) => (
    <div style={{ maxWidth: '20rem' }}>
      <Story />
    </div>
  ),
];

PrimaryCardNoCta.args = {
  cardSyles: ['primary'],
  h3: 'Title',
  body: '<div><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero id</p></div>',
};

PrimaryCardNoCta.decorators = [
  (Story) => (
    <div style={{ maxWidth: '20rem' }}>
      <Story />
    </div>
  ),
];

SecondaryCard.args = {
  cardSyles: ['secondary'],
  h2: 'h2 title',
  h4: 'Subtitle',
  imageSrc: 'https://picsum.photos/300/200',
  body: '<div><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar neque laoreet suspendisse interdum consectetur libero</p></div>',
  cta: <SecondaryButton {...SecondaryButton.args} />,
};

SecondaryCard.decorators = [
  (Story) => (
    <div style={{ maxWidth: '20rem' }}>
      <Story />
    </div>
  ),
];
