/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import Stripe from '../../../../core/react/Stripe';

import { DefaultTile } from './tiles.stories';

export default {
  title: 'Core/Stripe',
};

// eslint-disable-next-line react/jsx-props-no-spreading
const Template = (args) => <Stripe {...args} />;

export const DefaultStipe = Template.bind({});

DefaultStipe.args = {
  selections: ['default'],
  alignment: 'justify-center centered',
  heading2: 'What You need to Know About SCORE',
  headingAlign: 'center-fixed justify-center',
  subtitle:
    'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Egestas purus amet faucibus purus volutpat aliquam. Convallis facilisi sed ut quam quis neque risus et.',
  children: (
    <>
      <div className="w-3col">
        <DefaultTile {...DefaultTile.args} />
      </div>
      <div className="w-3col">
        <DefaultTile {...DefaultTile.args} />
      </div>
      <div className="w-3col">
        <DefaultTile {...DefaultTile.args} />
      </div>
    </>
  ),
};
