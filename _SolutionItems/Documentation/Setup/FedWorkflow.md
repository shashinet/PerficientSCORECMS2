# FED Workflow

- Work with your team to make components in [Storybook](https://storybook.js.org/).
  - [Write stories](https://storybook.js.org/docs/react/writing-stories/introduction) that allow you to test every variation of the components you might need, and verify that they satisfy all the requirements, and comply with accessibility rules.
  - Complex components should be written as stand-alone components javascript / javascript framework your project is using.
  - Simple components (that only require HTML and CSS) should be written directly in the Story files.
  - Style them using SCSS, being careful to follow standards and rules your FED team leader sets so your styling doesn't affect other components.
- The storybook is then "built" and given to product owner for review and to the Back End Developers (BEDs) for implementation.
  - BEDs will take the HTML from storybook to implement simple components in their CMS blocks.
- Complex components are bundled for deployment
  - Javascript and SCSS are compiled into a bundle that is deployed with the site, and referenced at runtime.
