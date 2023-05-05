import { addons } from '@storybook/addons';
import { themes } from '@storybook/theming';

import global from './themes/globalTheme';
import PerficientTheme from './themes/PerficientTheme';
import ExampleSiteTheme from './themes/ExampleSiteTheme';

const themeMap = {
  global: global,
  Perficient: PerficientTheme,
  ExampleSite: ExampleSiteTheme,
};

function setTheme(env) {
  let theme = env.split('/').pop();
  return themeMap[theme];
}

addons.setConfig({
  theme: setTheme(process.env.STORYBOOK_SITE),
});
