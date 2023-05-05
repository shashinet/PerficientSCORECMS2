/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import PictureStripe from '../../react/PictureStripe';
import './picture-stripe.scss';
import { PrimaryCardHorizontal } from '../cards/card.stories';

export default {
  title: 'Blocks/PictureStripe',
  component: PictureStripe,
  argTypes: {
    backgroundColor: { control: { type: 'color' } },
    children: { control: { type: 'text' } },
    color: { control: { type: 'color' } },
  },
  parameters: {
    docs: {
      description: {
        component:
          '## PictureStripe is a div that can have a color, backgroundColor, backgroundImage, or style pictureStripeStyles pass in',
      },
      source: {
        code: PictureStripe,
      },
    },
  },
};

const Template = (args) => <PictureStripe {...args} />;

export const Default = Template.bind({});

Default.args = {
  color: '#000',
  backgroundColor: '#fff',
  alignment: 'justify-center',
  backgroundImage: [
    {
      alt: '',
      srcSet: 'https://picsum.photos/id/1002/1400/300',
      media: 'image/jpeg',
      minWidth: '1200',
      type: 'image/jpeg',
    },
    {
      alt: '',
      srcSet: 'https://picsum.photos/id/100/1000/300',
      media: 'image/jpeg',
      minWidth: '767',
      type: 'image/jpeg',
    },
    {
      alt: '',
      srcSet: 'https://picsum.photos/id/0/400/200',
      media: 'image/jpeg',
      minWidth: '600',
      type: 'image/jpeg',
    },
  ],
  fallBackSrc: 'https://picsum.photos/id/1/100/300',
  pictureStripeStyles: ['default'],
  children: (
    <>
      <div className="w-4col"><PrimaryCardHorizontal {...PrimaryCardHorizontal.args} /></div>
      <div className="w-4col"><PrimaryCardHorizontal {...PrimaryCardHorizontal.args} /></div>
      <div className="w-4col"><PrimaryCardHorizontal {...PrimaryCardHorizontal.args} /></div>
    </>),
};
