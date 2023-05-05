# Assigning your certificate to Kestrel

1. In Visual Studio, open your appsettings file containing your **Kestrel Endpoints configuration**.

2. For each Endpoint you want to use a certificate for, use the pattern below to add a "**Certificate**" entry below the Url entry:

    ```json
    "Perficient Secure": {
        "Url": "https://perficient.local:4401",
        "Certificate": {
            "Subject": "perficient.local",
            "Store": "MY",
            "Location": "LocalMachine"
        }
    }
    ```
