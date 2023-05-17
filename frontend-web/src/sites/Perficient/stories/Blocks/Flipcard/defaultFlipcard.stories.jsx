import React from 'react';
import DefaultFlipcard from '../../../../../core/react/Flipcard/flipcard';

export default {
  title: 'Blocks/Flipcard',
  component: DefaultFlipcard,
  parameters: {
    layout: 'fullscreen',
   
    design: {
      type: 'figma',
      url: 'https://www.figma.com/file/yNiX9ZP9fXMaidWaMOjzDh/Design?node-id=3212%3A108604',
      allowfullscreen: true,
    },
  },
};

// eslint-disable-next-line react/jsx-props-no-spreading
const Template = (args) => <DefaultFlipcard {...args} />;

export const DefaultFlipcardBlock = Template.bind({});
DefaultFlipcardBlock.args = {
  
};
