/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import Stripe from '../../react/Stripe';
import { SectionHeroBlock } from '../sectionHero/sectionHeroBlock.stories';
import { PrimaryCard } from '../cards/card.stories';
import { PrimaryButton } from '../buttonLink/buttonLink.stories';
import '../../../global/styles/index.scss';
import './stripe.scss';
import { Accent } from '../richText/richText.stories';

export default {
  title: 'Components/Stripe',
  component: Stripe,
  argTypes: {
    backgroundColor: { control: { type: 'color' } },
    backgroundImage: { control: { type: 'file' } },
    children: { control: { type: 'text' } },
    color: { control: { type: 'color' } },
  },
  parameters: {
    layout: 'fullscreen',
  },
};

const Template = (args) => <Stripe {...args} />;

export const Default = Template.bind({});
export const DefaultCenter = Template.bind({});
export const DefaultRightContent = Template.bind({});
export const DefaultContent = Template.bind({});
export const DefaultContentWideCenter = Template.bind({});

Default.args = {
  stripeStyles: ['default'],
  color: '#000',
  backgroundColor: '#fff',
  heading2: 'Stripe Heading 2',
  children: (
    <>
      <div className="w-3col">
        <PrimaryCard {...PrimaryCard.args} />
      </div>
      <div className="w-3col">
        <PrimaryCard {...PrimaryCard.args} />
      </div>
      <div className="w-3col">
        <PrimaryCard {...PrimaryCard.args} />
      </div>
    </>
  ),
  cta: (
    <>
      <PrimaryButton {...PrimaryButton.args} />
    </>
  ),
};

DefaultCenter.args = {
  stripeStyles: ['default'],
  color: '#000',
  backgroundColor: '#fff',
  heading2: 'Stripe Heading 2',
  headingAlign: 'center',
  ctaAlign: 'center',
  alignment: 'justify-center',
  children: (
    <>
      <div className="w-3col">
        <PrimaryCard {...PrimaryCard.args} />
      </div>
      <div className="w-3col">
        <PrimaryCard {...PrimaryCard.args} />
      </div>
      <div className="w-3col">
        <PrimaryCard {...PrimaryCard.args} />
      </div>
    </>
  ),
  cta: (
    <>
      <PrimaryButton {...PrimaryButton.args} />
    </>
  ),
};

DefaultRightContent.args = {
  stripeStyles: ['default'],
  color: '#000',
  backgroundColor: '#fff',
  heading2: 'Stripe Heading 2',
  headingAlign: 'center',
  ctaAlign: 'center',
  alignment: 'justify-end',
  children: (
    <>
      <div className="w-8col">
        <SectionHeroBlock {...SectionHeroBlock.args} />
      </div>
      <div className="w-3col">
        <Accent {...Accent.args} />
      </div>
    </>
  ),
  cta: (
    <>
      <PrimaryButton {...PrimaryButton.args} />
    </>
  ),
};

DefaultContent.args = {
  stripeStyles: ['default'],
  color: '#000',
  backgroundColor: '#fff',
  heading2: 'Stripe Heading 2',
  headingAlign: 'center',
  ctaAlign: 'center',
  children: (
    <>
      <div className="w-9col">
        <SectionHeroBlock {...SectionHeroBlock.args} />
      </div>
      <div className="w-3col">
        <Accent {...Accent.args} />
      </div>
    </>
  ),
  cta: (
    <>
      <PrimaryButton {...PrimaryButton.args} />
    </>
  ),
};

DefaultContentWideCenter.args = {
  stripeStyles: ['default'],
  color: '#000',
  backgroundColor: '#fff',
  heading2: 'Stripe Heading 2',
  alignment: 'justify-center',
  headingAlign: 'center',
  ctaAlign: 'center',
  children: (
    <>
      <div className="w-8col">
        <SectionHeroBlock {...SectionHeroBlock.args} />
      </div>
    </>
  ),
  cta: (
    <>
      <PrimaryButton {...PrimaryButton.args} />
    </>
  ),
};
