# Update App Settings for HTTPS

1. Locate your **ConnectionStrings** entry in the appropriate appsettings file

2. Enabled Trusted certificates by adding the following to the EPiSErverDB connection string: `TrustServerCertificate=True;`

    ![](./dev/_SolutionItems/Documentation/Images/certs/2023-03-15-23-23-10-image.png)

3. Save the file

4. You should be able to run your application now

&nbsp;
