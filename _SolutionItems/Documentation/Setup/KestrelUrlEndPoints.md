# Configuring URL Endpoints

The "**externalUrlConfiguration**" option in **launchSettings.json** allows us to configure endpoints in the appSettings file of our choice. A global configuration can be specified in the **appsettings.Development.json** file to affect all developers working with the solution. A local specific configuration can be specified for eacn developer, unique to their local solution, by using the **appsettings.local.json**.

## Configure Kestrel Endpoints following these steps

1. Open the configuration of your choice. For a new project, start with **appsettings.Development.json** to establish a project default. For a developer specific configuration choose **appsettings.local.json**

2. Insert a "**Kestrel**" section near the top with an "**Endpoints**" subsection. If an "**AllowedHosts**" entry exists, place it below that for ease.

3. Each endpoint is defined with a name marking the entry, and a Url property specifying the URL domain. Each Url must have a unique port number for Kestrel to map the listener.

    A good practice is to insert a "**Default**" entry matching the **launchUrl** in your **launchSettings.json** configuration. This allows the launchUrl to load, and works as a good test to indicate when the Kestrel server is initialized.

    ```json
    // this should be your starting point
    {
     "AllowedHosts": "*",
     "Kestrel": {
      "Endpoints": {
       "Default": {
        "Url": "https://localhost:4431"
       }
      }
     }
    }
    ```

4. Any additional Urls or custom hostnames needed for your project can be added to the Endpoints configuration. Ensure that each one uses a unique port number.

    ```json
    // example project with multiple endpoints
    {
     "AllowedHosts": "*",
     "Kestrel": {
      "Endpoints": {
       "Default": {
        "Url": "https://localhost:4431"
       },
       "MyProject Unsecure": {
        "Url": "http://myproject.local:5000"
       },
       "MyProject Secure": {
        "Url": "https://myproject.local:4401"
       },
       "MyProject Subdomain": {
        "Url": "https://intranet.myproject.local:4402"
       }
      }
     }
    }
    ```

**_Note_**: _I use ".local" for my local development domains but you can use any suffix you like (".com", ".net", etc...) if you map it in your **hosts** file._
