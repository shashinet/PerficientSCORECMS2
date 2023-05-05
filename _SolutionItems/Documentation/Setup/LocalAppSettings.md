# Local App Settings

The project configuration is set up to support global Development settings and local developer settings.

Development settings are managed using the **appsettings.Development.json** file in the Web project root. This file should be used to specify settings that all developers on the project need when operating with the solution. Examples of these kinds of settings are SSO options, block configuration options, client specific settings, etc... **The appsettings.Development.json file is tracked by source control.**

Any of the global Development settings can be overridden locally, specific to a developer, by using an **appsettings.local.json** file created in the Web project root. Any values specified in a local appsettings file will override all other configuration values locally. This is useful when a developer wants to manage a locally specific value. Examples of these kinds of settings are Kestrel Endpoints and certificates, Find settings, logging options, etc... **The appsettings.local.json file is NOT tracked by source control. It is ignored.**

## Creating your local appsettings file:

1. Navigate to the *\\*_SolutionItems\Configuration folder within from the repo root

2. Copy the appsettings.local.json.template file and paste it in the Web project folder (def: src\Web)

3. Remove the **.template** extension from the pasted file

4. To get your site running, you will need to update your the Database Connection String with the name of your server (replace the [database server] ).

5. Modify, remove, or add any settings and options values you need to manage specific to your local environment.

## Updating the Find Index

1. Go to the [Search & Navigation Developer Demo](https://find.episerver.com/) Site and create a new demo index.

2. Update the appsettings.local.json file with the new DefaultIndex and Service Url settings.
