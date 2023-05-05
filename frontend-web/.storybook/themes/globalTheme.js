import { create } from '@storybook/theming/create';

export default create({
  base: 'light',

  colorPrimary: '#000000',
  colorSecondary: '#cc1f20',

  // UI
  appBg: '#FFFFFF',
  appContentBg: '#FFFFFF',
  appBorderColor: '#7e8282',
  appBorderRadius: 4,

  // Typography
  fontBase: '"Open Sans", sans-serif',
  fontCode: 'monospace',

  // Text colors
  textColor: '#252727',
  textInverseColor: '#cc1f20',

  // Toolbar default and active colors
  barTextColor: '#FFFFFF',
  barSelectedColor: '#7e8282',
  barBg: '#252727',

  // Form colors
  inputBg: '#252727',
  inputBorder: '#CC1F20',
  inputTextColor: '#FFFFFF',
  inputBorderRadius: 8,

  brandTitle: 'Storybook | Your Site Name ',
  brandImage: 'https://Perficient.org/Layout/images/logo-cc.png'
});
