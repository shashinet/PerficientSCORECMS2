/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import SectionHeroBlockComponent from '../../react/SectionHero';
import ButtonLink from '../../react/ButtonLink';

export default {
  title: 'Blocks/SectionHero',
  parameters: {},
};

const Template = (args) => <SectionHeroBlockComponent {...args} />;
export const SectionHeroBlock = Template.bind({});

SectionHeroBlock.args = {
  imageSrc: 'https://picsum.photos/500/300',
  imageAlt: 'Alt text goes herea',
  contentType: 'Image',
  h2: 'Urgent Care Brand',
  body: '<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>',
  cta: <ButtonLink title="Find Urgent Care" url="www.google.com" />,
};
