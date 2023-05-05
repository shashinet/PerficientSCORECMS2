# Setup Host File Records

A host file record will be required for each domain name you wish to browse locally. In the above example the "Unsecure" and "Secure" entries use different ports, but the same domain name.

1. Open the **c:\windows\system32\drivers\etc\hosts** file in admin mode (Notepad ++ is useful for this).

2. Add a line to it for each domain your using, specifying the local IP (127.0.0.1) with the domain:

```bash
127.0.0.1        perficient.local        # you can add comments
```

3. Save the hosts file.
