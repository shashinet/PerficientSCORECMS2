import React from 'react';
import Tile from '../../react/Tile';
import './tile.scss';

export default {
  title: 'Components/DefaultTile',
  component: Tile,
  argTypes: {
    selections: {
      control: {
        type: '',
        options: ['default'],
      },
      description: 'Tile class selection',
      default: 'default',
    },
    imgSrc: {
      control: {
        type: 'file',
      },
    },
    title: {
      control: {
        type: 'text',
      },
    },
    parameters: {
      docs: {
        description: {
          component: '## Tile has a title and image and clickable link  that can have style selections',
        },
        source: {
          code: Tile,
        },
      },
    },
  },
};

const Template = (args) => <Tile {...args} />;

export const DefaultTile = Template.bind({});

DefaultTile.args = {
  block: {
    selections: ['default'],
    href: 'www.google.com',
    imgSrc: 'https://picsum.photos/80/80',
    altText: 'some Alt text here',
    title: 'Tiles are clickable',
  }
};
DefaultTile.decorators = [
  (Story) => (
    <div style={{ maxWidth: '20rem' }}>
      <Story/>
    </div>
  )
];
