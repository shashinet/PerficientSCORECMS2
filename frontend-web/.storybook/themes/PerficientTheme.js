import { create } from '@storybook/theming/create';
import headerLogo from '../../images/prft-logo.png';

export default create({
  base: 'light',

  colorPrimary: '#010101',
  colorSecondary: '#CC1F20',

  // UI
  appBg: '#f5f7f7',
  appContentBg: '#f5f7f7',
  appBorderColor: '#8C734B',
  appBorderRadius: 8,

  // Typography
  fontBase: '"Work Sans", sans-serif',
  fontCode: 'monospace',

  // Text colors
  textColor: '#222222',
  textInverseColor: '#ffffff',

  // Toolbar default and active colors
  barTextColor: '#8D0E11',
  barSelectedColor: '#E61717',
  barBg: '#f5f5f7',

  // Form colors
  inputBg: '#ffffff',
  inputBorder: '#8D0E11',
  inputTextColor: '#8D0E11',
  inputBorderRadius: 8,

  brandTitle: 'Perficient',
  brandImage: headerLogo,
});
