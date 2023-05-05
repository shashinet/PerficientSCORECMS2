import React from 'react';
import { Default } from '../../../../core/stories/richText/richText.stories';

export default {
  title: 'Core/Rich Text',
  component: Default,
};

export const ContentSpot = () => <Default {...Default.args} />;
