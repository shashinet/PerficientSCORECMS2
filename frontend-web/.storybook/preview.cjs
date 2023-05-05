import 'what-input';

if (import.meta.env.STORYBOOK_BUILD) {
  // If this is a "storybook build", it'll be put inside the opti backend wwwroot folder.
  // From there we can reference the global styles that should have also been built there by the regular build process.
  const link = document.createElement('link');
  link.rel = 'stylesheet';
  link.href = `./dist/${import.meta.env.STORYBOOK_SITE}/style.css`;
  document.head.appendChild(link);
} else {
  // Workaround to avoid Vite's issues with dynamic inputs
  const sitePathSegments = import.meta.env.STORYBOOK_SITE.split('/');
  const globalStylePath = `../src/${sitePathSegments[0]}/${sitePathSegments[1]}/styles/index.scss`;
  import(/* @vite-ignore */ globalStylePath);
}

const customViewports = {
  iPhoneX: {
    name: 'iPhone X',
    styles: {
      width: '375px',
      height: '812px',
    },
  },
  iPhone11Pro: {
    name: 'iPhone 11 pro',
    styles: {
      width: '414px',
      height: '763px',
    },
  },
  iPad: {
    name: 'iPad',
    styles: {
      width: '768px',
      height: '1024px',
    },
  },
  LargeTablet: {
    name: 'Large Tablet',
    styles: {
      width: '992px',
      height: '1024px',
    },
  },
  SmallDesktop: {
    name: 'Small Desktop',
    styles: {
      width: '1200px',
      height: '1024px',
    },
  },
  lapTopHiDpiScreen: {
    name: 'Laptop with HiPDI screen',
    styles: {
      width: '1440px',
      height: '900px',
    },
  },
  LargeDesktop: {
    name: '1920 Desktop',
    styles: {
      width: '1920px',
      height: '1200px',
    },
  },
};

export const parameters = {
  actions: { argTypesRegex: '^on[A-Z].*' },
  controls: {
    expanded: true,
    matchers: {
      color: /(background|color)$/i,
      date: /Date$/,
    },
    presetColors: [
      //Add your custom brand colors here
      {
        color: '#252727',
        title: 'Black',
      },
      {
        color: '#545859',
        title: 'Gray',
      },
      {
        color: '#888b8d',
        title: 'Mid-Gray',
      },
      {
        color: '#f5f5f7',
        title: 'Light Grey',
      },
      {
        color: '#ffffff',
        title: 'White',
      },
    ],
  },
  options: {
    // You can create story order below
    storySort: {
      order: ['Information', 'Components'],
    },
  },
  viewport: {
    viewports: {
      ...customViewports,
    },
  },
  backgrounds: {
    // Add your custom background colors here
    default: 'white',
    values: [
      {
        name: 'white',
        value: '#FFFFFF',
      },
      {
        name: 'lightgrey',
        value: '#f5f5f7',
      },
      {
        name: 'aubergine',
        value: '#512d6d',
      },
      {
        name: 'magenta',
        value: '#c81e82',
      },
      {
        name: 'ivoryaubergine',
        value: '#f0e6ff',
      },
    ],
  },
  html: {
    prettier: {
      tabWidth: 4,
      useTabs: false,
      htmlWhitespaceSensitivity: 'strict',
    },
  },
};
