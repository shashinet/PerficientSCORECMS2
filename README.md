
# Overview

[Update this with an overview of the project]

&nbsp;

&nbsp;

## Development Tools

### Install the following software

- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) _- necessary for .NET 6+ and CMS 12 development_
- [SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) _- DO NOT USE SQL EXPRESS_
- [SQL Studio Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [VS Code](https://code.visualstudio.com/download)
- [Node.js](https://nodejs.org/en/download/) _- The Node versions this project were tested on (at the time this was written) were 16.19.3 and 18.15.0, but higher versions will probably work._
- [Node Version Manager](https://github.com/coreybutler/nvm-windows/releases) OR [Volta](https://volta.sh/) _- optional but useful for managing different node versions_

&nbsp;

## Setting up the Local Environment

### Repository Setup

Clone the repository to your local dev machine. It is reccomended that you clone this to and easibly accessable location such as C:\Dev. After clonging the repository the settings should be updated to run the solution.

- [Setup a local App Settings File](./_SolutionItems/Documentation/Setup/LocalAppSettings.md)
- Kestrel Configuration should be done by the architect during the code setup. The following outlines the steps to setup Kestrel
  - [Configure Launch Settings](./_SolutionItems/Documentation/Setup/KestrelLaunchSettings.md)
  - [Configure Kestrel Endpoints](./_SolutionItems/Documentation/Setup/KestrelUrlEndPoints.md)

### Configuration

- [Configure Visual Studio](./_SolutionItems/Documentation/Setup/ConfigureVisualStudio.md)
- [Configure Visual Studio Code](./_SolutionItems/Documentation/Setup/ConfigureVisualStudioCode.md)
- [Configure Sql Server](./_SolutionItems/Documentation/Setup/ConfigureSqlServer.md)
- [Setup Host File Entries](./_SolutionItems/Documentation/Setup/HostFile.md)

### Site Setup

- [Create the Database](./_SolutionItems/Documentation/Setup/CreateDatabase.md)
- [Setup the Front End Code](./_SolutionItems/Documentation/Setup/SetupFedCode.md)
- [Run the Application in Kestrel](./_SolutionItems/Documentation/Setup/KestrelRun.md)
- [Setting Up the Site in the CMS](./_SolutionItems/Documentation/Setup/SiteSetup.md)

&nbsp;

&nbsp;

## Front End Development
Front End Development relies heavily on Storybook to provide a faster environment for the FEDs to do development and provide a faster feedback cycle with the product owner.

- [Front End Development Workflow](./_SolutionItems/Documentation/Setup/FedWorkflow.md)
- [Front End Commands](./_SolutionItems/Documentation/Setup/FedCommands.md)

&nbsp;

&nbsp;

## Alternative Configurations

### Adding HTTPS Support For Kestrel

HTTPS may not be required for your local development set up.  If needed, follow these steps:

- [Create Local Certificates](./_SolutionItems/Documentation/Setup/HttpsCreateCertificates.md)
- [Configuring SQL Server for HTTPS](./_SolutionItems/Documentation/Setup/HttpsConfigureSqlServer.md)
- [Configuring Kestrel HTTPS](./_SolutionItems/Documentation/Setup/HttpsKetrelConfigure.md)
- [Update AppSettings for  HTTPS](./_SolutionItems/Documentation/Setup/HttpsAppSettings.md)

### Running the solution in IIS

It is prefered to run Kestrel when possible as no local deployments are neccesary thus reducing time. In some instances it may be required to run the application in IIS.  If needed the following documenatation can be used to setup the local IIS instance.

- [Installing IIS](./_SolutionItems/Documentation/Setup/HttpsAppSettings.md)
- [Publishing the Site](./_SolutionItems/Documentation/Setup/HttpsAppSettings.md)

## Troubleshooting

- [View the troubleshooting page for common issues](./_SolutionItems/Documentation/Setup/Troublshooting.md)
