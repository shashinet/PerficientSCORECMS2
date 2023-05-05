import { defineConfig, loadEnv } from 'vite';
import react from '@vitejs/plugin-react';
import autoprefixer from 'autoprefixer';

export default ({ mode }) => {
  process.env = { ...process.env, ...loadEnv(mode, process.cwd()) };

  const siteRoot = `src/${process.env.SITE.trim()}`;
  const sitePublicFolder = `public`; // relative to siteRoot

  console.log('=======');
  console.log('Root folder: ');
  console.log(siteRoot);
  console.log('Public folder: ');
  console.log(siteRoot + '/' + sitePublicFolder);
  console.log('=======');

  // https://vitejs.dev/config/
  return defineConfig({
    plugins: [react()],
    root: siteRoot,
    publicDir: sitePublicFolder, // relative to siteRoot
    css: {
      postcss: {
        plugins: [autoprefixer({})],
      },
    },
    build: {
      emptyOutDir: true,
      sourcemap: process.env.MODE === 'development',
      cssMinify: !(process.env.MODE === 'development'),
      cssCodeSplit: false, // makes vite keep the CSS in one file
      minify: !(process.env.MODE === 'development'),
      outDir: `../../../../dev/src/Web/wwwroot/dist/${process.env.SITE}`,
      rollupOptions: {
        output: {
          entryFileNames: 'main.js',
          assetFileNames: '[name].[ext]',
        },
      },
    },
  });
};
