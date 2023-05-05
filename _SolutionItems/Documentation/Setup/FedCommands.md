## NPM Commands for FEDs

## Running Storybook

Storybook has been configured to run on a site-instance. To run Storybook, run the following command in a terminal session from `./frontend-web/`:

```sh
npm run dev:ExampleSiteName
```

This will launch Storybook on `http://localhost:6001/` and serve static assets from the `./frontend-web/public` directory. Note: assets in `public/` are nested in a site-specific context, e.g. `/public/ExampleSiteName`.

To add new stories, add them in an appropriate directory under: `./frontend-web/src/sites/ExampleSiteName/stories/`. Name the file, `*.stories.jsx`, and the file will be auto imported at runtime into Storybook.

## Building the Storybook for sharing

Each site will be configured with a build command for building it's Storybook. This will build a self-contained copy of the Storybook into the `./dev/src/Web/wwwroot/dist/Assets/Storybook`, which the customer can look at for approval, and the BEDs can use as a reference for implementing your components.

```sh
npm run storybookBuild:Perficient
```

## Building for deployment

Each site will be configured with two build scripts. One to build in production mode and one in development mode. Each will transpile the appropriate bundle into the `./dev/src/Web/wwwroot/dist/Assets/Storybook`, but the development version will be unminified. This will also copy, site-specific static assets.

```sh
npm run build:ExampleSiteName # Prod build
npm run build:ExampleSiteName:dev # Dev build
```

## Javascript Linting

Linting has been configured to run automatically in Storybook, on a Webpack build, and on a Git `pre-commit` hook. To manually run the linter, use the following command:

```sh
npm run lint
```

## SCSS Linting

SCSS Linting can be done manually, to help you discover bugs in your SCSS.

```sh
npm run stylelint:ExampleSiteName
```
