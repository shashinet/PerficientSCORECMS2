/* eslint-disable jsx-a11y/anchor-is-valid */
import React from 'react';
import Accordion from '../../react/Accordion';
import './accordion.scss';

export default {
  title: 'Components/Accordion',
  components: Accordion
};

const Template = (args) => <Accordion {...args} />;

export const PrimaryAccordion = Template.bind({});
PrimaryAccordion.args = {
  accordionStyles: ['default'],
  panel: [
    {
      title: 'Accordion Label One',
      panelContent: '<h3>Accordion Inner Title</h3><p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Autem consequuntur dignissimos doloribus earum ex explicabo id iste itaque officia officiis quae quam quo quos repellendus reprehenderit, suscipit vitae voluptatibus voluptatum.</p>'
    },
    {
      title: 'Accordion Label Two',
      panelContent: '<h3>Accordion Inner Title</h3><p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Autem consequuntur dignissimos doloribus earum ex explicabo id iste itaque officia officiis quae quam quo quos repellendus reprehenderit, suscipit vitae voluptatibus voluptatum.</p>'
    }
  ],
};
