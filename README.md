Patros.AuthenticatedHttpClient
==============================

A collection of helpers to create HttpClient instances that automatically
handle authentication for you.

Azure AD Authenticated Http Client Example Usage
------------------------------------------------

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new AzureAdAuthenticatedHttpClientOptions
{
    // this is the default value for AadInstance and can be omitted if you don't need to change it
    AadInstance = "https://login.microsoftonline.com/{0}",
    Tenant = "YOUR AZURE AD TENANT ID (GUID)",
    ClientId = "CLIENT APPLICATION ID (GUID)",
    AppKey = "CLIENT SECRET",
    ResourceId = "APPLICATION ID OF THE SERVICE YOU ARE AUTHENTICATING TO (GUID)"
};

var client = AzureAdAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync();
```

Basic Authenticated Http Client Example Usage
---------------------------------------------

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new BasicAuthenticatedHttpClientOptions
{
    UserId = "INSERT YOUR USERNAME HERE",
    Password = "INSERT YOUR PASSWORD HERE"
};

var client = BasicAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync();
```