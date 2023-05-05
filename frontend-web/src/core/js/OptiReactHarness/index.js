import React from 'react';
import * as ReactDOMClient from 'react-dom/client';

export default function optiReactHarness(ComponentTypes) {
  document.addEventListener('DOMContentLoaded', () => {
    function initialize(componentContainer) {
      try {
        const props = JSON.parse(componentContainer.dataset.props || '{}');
        const arr = Array.from(componentContainer.children);
        props.propertyName = componentContainer.dataset.epiPropertyName;
        props['children'] = arr.length > 0 ? arr : []; // eslint-disable-line dot-notation

        const root = ReactDOMClient.createRoot(componentContainer);
        root.render(
          React.createElement(ComponentTypes[componentContainer.dataset.reactComponent], props),
        );

        // eslint-disable-next-line no-param-reassign
        componentContainer.dataset.props = 'loaded';
      } catch (error) {
        // eslint-disable-next-line no-console
        console.error('Could not mount component', error);
      }
    }

    let reactComponents = document.querySelectorAll('[data-react-component]');

    reactComponents = Array.prototype.slice.call(reactComponents);
    reactComponents.forEach(initialize);
  });
}
