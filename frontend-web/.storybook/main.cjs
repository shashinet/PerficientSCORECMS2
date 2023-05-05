const path = require('path');

const siteStories = `../src/${process.env.STORYBOOK_SITE.trim()}/**/*.stories.@(jsx|js)`;
const siteDocs = `../src/${process.env.STORYBOOK_SITE.trim()}/**/*.stories.mdx`;
const sitePublicFolder = `../src/${process.env.STORYBOOK_SITE.trim()}/public`;

console.log('=======');
console.log('Stories folder: ');
console.log(siteStories);
console.log('Docs folder: ');
console.log(siteDocs);
console.log('Public folder: ');
console.log(sitePublicFolder);
console.log('=======');

module.exports = {
  stories: [siteDocs, siteStories],
  staticDirs: [sitePublicFolder],
  addons: [
    '@whitespace/storybook-addon-html',
    {
      name: '@storybook/addon-essentials',
      options: {
        actions: false,
      },
    },
    '@storybook/addon-a11y',
    '@storybook/addon-docs',
  ],
  framework: '@storybook/react',
  core: {
    builder: '@storybook/builder-vite',
  },
  features: {
    storyStoreV7: true,
  },
};
