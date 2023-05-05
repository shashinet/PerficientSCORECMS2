/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import Card from '../../../../core/react/Card';
import ButtonLink from '../../../../core/react/ButtonLink';

export default {
  title: 'Core/Card',
  component: Card,
  argTypes: {
    imageSrc: {
      control: {
        type: 'file',
      },
    },
    body: {
      control: {
        type: 'text',
      },
    },
    cta: {
      control: {
        type: 'text',
      },
    },
    description: 'Card image select from local files',
    default: 'default',
  },
  parameters: {
    docs: {
      description: {
        component:
          '## Card is a component with heading, h2, image, body(rte), cta that can have style selection passed in',
      },
      source: {
        code: Card,
      },
    },
  },
};

const Template = (args) => (
  <>
    <div className="w-4col">
      <Card {...args} />
    </div>
  </>
);

export const DefaultCard = Template.bind({});
export const FlushImage = Template.bind({});
export const OverDark = Template.bind({});
export const PrimaryProfile = Template.bind({});
export const LargeProfile = Template.bind({});
export const SimpleCard = Template.bind({});

DefaultCard.args = {
  cardStyles: ['default'],
  header: 'header',
  h2: 'This is a long heading for a news card',
  image: {
    imageSrc: 'https://picsum.photos/400',
    altText: 'some thing to see here',
    contentType: 'Image',
  },
  body: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non, rutrum magna.',
  cta: <ButtonLink selections={['text-primary']} title="Read More" />,
};

FlushImage.args = {
  cardStyles: ['flush-image'],
  header: 'header',
  h2: 'This is a long heading for a news card',
  image: {
    imageSrc: 'https://picsum.photos/400',
    altText: 'some thing to see here',
    contentType: 'Image',
  },
  body: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non, rutrum magna.',
  cta: <ButtonLink selections={['text-primary']} title="Read More" />,
};

OverDark.args = {
  cardStyles: ['over-dark'],
  header: 'header',
  h2: 'This is a long heading for a news card',
  image: {
    imageSrc: 'https://picsum.photos/400',
    altText: 'some thing to see here',
    contentType: 'Image',
  },
  body: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non, rutrum magna.',
  cta: <ButtonLink selections={['text-primary']} title="Read More" />,
};

PrimaryProfile.args = {
  cardStyles: ['primary-profile'],
  h2: 'Daved Artemik',
  image: {
    imageSrc: 'https://picsum.photos/100',
    altText: 'some thing to see here',
    contentType: 'Image',
  },
  body: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis.',
  cta: <ButtonLink selections={['text-primary']} title="View Profile" />,
};

LargeProfile.args = {
  cardStyles: ['large-profile'],
  h2: 'Mike Baker',
  subHeading: 'UI Architect',
  image: {
    imageSrc: 'https://picsum.photos/200/300',
    altText: 'some thing to see here',
    contentType: 'Image',
  },
  body: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque elit sit ut vestibulum, amet, eget porttitor. Eget nunc convallis magna dolor non. Arcu tincidunt sagittis, quam tellus fringilla.',
};

SimpleCard.args = {
  cardStyles: ['simple-card'],
  h2: 'Ask an Expert',
  image: {
    imageSrc: 'https://picsum.photos/100',
    altText: 'some thing to see here',
    contentType: 'Image',
  },
  body: 'Brand Message about Perficient. Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
  cta: <ButtonLink selections={['text-primary']} title="View Profile" />,
};
