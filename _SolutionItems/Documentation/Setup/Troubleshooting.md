# Troubleshooting Common Setup Issues

1. _No process is on the other end of the pipe_
   Fixed by setting **Target Runtime** in the Publish Profile to **win-x64** and/or setting "Enable 32-Bit Applications" to false in your IIS site's Application Pool Advanced Settings.
2. When setting up HTTPS inside IIS, it says:

    > "At least one other site is using the same HTTPS binding"

    1. Click "No" to return to the setup.
    2. Make sure "Require Server Name Indication" is checked.

3. Error message appears that says:

    > Access to the path 'C:\inetpub\wwwroot\Perficient\modules_protected\EPiServer.Labs.LanguageManager\\Translation' is denied.

    1. Create a folder for the Perficient project at `C:\inetpub\wwwroot\Perficient`.
    2. Right click on the new Perficient folder and go to "Properties".
    3. Go to the Security tab and click "Edit".
    4. "Add" a new entry.
    5. Change the location to your local computer and type "IIS AppPool\\perficient.local" in the object names box. (Click "Check Names" to make sure it was typed correctly.)
    6. Give this user Full Control to the Perficient folder so it can write logs.

4. When cloning into Visual Studio, if your account is not logged into the right domain to access the repository, it will say:

    > TF30063: You are not authorized to access {server}.

    1. Inside Visual Studio, open the Team Explorer window.
    2. Go to "Manage Connections" in the ribbon bar.
    3. Click "Manage Connections" > "Connect to a Project".
    4. Select the account to access the repository.
    5. Log in again, if needed.
    6. Select the repo under the server 'perficient-msftnbu.visualstudio.com'.
    7. Update the location on your computer to clone the repository.
    8. Click "Clone".

5. When accessing the site through HTTPS, all links return a `403: Forbidden` error.
    1. First, check to make sure you are on the VPN.
    2. If that doesn't resolve the issue, go to https://find.episerver.com and create a new demo index.
    3. Add the new demo index to the web.config file found in the solution's root directory

6. User can log into the editor but all pages render with an unstyled, Courier New font, "Page not found" message. On-page editing shows the same message.
    1. Inside the editor, go to Admin > Config > Manage Websites
    2. Verify that the "Score Training" site is pointing to http://perficient.local and that there isn't any spelling error.
