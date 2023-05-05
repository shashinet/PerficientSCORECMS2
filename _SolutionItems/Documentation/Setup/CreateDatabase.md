# Create the CMS Database

In Powershell navigate to the solutions root directory and run the following command:

```sh
dotnet-episerver create-cms-database ".\dev\src\Web\Perficient.Web.csproj" -S . -dn "Perficient.Cms" -E -du optimizelyUser -dp P@ssw0rd!
```

Note that using optimizelyUser will necessitate a change to the connectiongString in appSettings until we can clean up this readme.