Patros.AuthenticatedHttpClient
==============================

A collection of helpers to create HttpClient instances that automatically
handle authentication for you.

So far the only complete one is for Azure AD authentication using OAuth.

I've started the basic one but it needs some work to correctly respond to
401 responses and the realm parameter.

Azure AD Authenticated Http Client Example Usage
------------------------------------------------

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new AzureAdAuthenticatedHttpClientOptions
{
    // this is the default value and can be omitted if you don't need to change it
    AadInstance = "https://login.microsoftonline.com/{0}",
    Tenant = "YOUR AZURE AD TENANT ID (GUID)",
    ClientId = "CLIENT APPLICATION ID (GUID)",
    AppKey = "CLIENT SECRET",
    ResourceId = "APPLICATION ID OF THE SERVICE YOU ARE AUTHENTICATING TO (GUID)"
};

var client = AzureAdAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync();
```