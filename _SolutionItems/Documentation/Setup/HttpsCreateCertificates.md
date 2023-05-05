# Creating Certificates

Browsing a secure (HTTPS) Url might throw a certificate warning in your browser. You can most often bypass this in the browser with a "_Proceed at your own risk_" option. However, you can also generate and assign a self-signed certificate for local development to avoid this.

**_Note:_** _Do not use the "Create self-signed certificate" function inside IIS as it will not be bound to the project hostname._

## Create a self-signed certificate

1. Open **Powershell as Administrator**

2. Follow command **A** for a single domain project, or command **B** to support subdomain Urls.

    a. Single Domain - single entry for DnsName argument:

    ```bash
    New-SelfSignedCertificate -DnsName perficient.local -CertStoreLocation "cert:\LocalMachine\My"
    ```
<!-- 
    b. Sub-domains - add multiple domains to a single cert by comma separating, and use \* for wildcard:

    ```bash
    New-SelfSignedCertificate -subject "perficient.local" -dnsname perficient.local, *.perficient.local -CertStoreLocation "cert:\LocalMachine\My"
    ``` -->
