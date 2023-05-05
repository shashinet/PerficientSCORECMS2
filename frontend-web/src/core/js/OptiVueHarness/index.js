// Note that the ESM version of Vue is used here, which is required
// for parsing the Vue components directly from the HTML DOM.
import { createApp } from 'vue/dist/vue.esm-bundler';

export default function optiVueHarness(ComponentTypes) {
  document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('[data-vue-component]').forEach((mountPoint) => {
      try {
        const app = createApp();
        Object.keys(ComponentTypes).forEach((key) => {
          app.component(key, ComponentTypes[key]);
        });
        app.mount(mountPoint);
      } catch (e) {
        // eslint-disable-next-line no-console
        console.error('Error mounting Vue component', e);
      }
    });
  });
}
