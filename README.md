Authenticated HttpClient [![Build status](https://dev.azure.com/patros/OpenSource/_apis/build/status/Patros.AuthenticatedHttpClient?branchName=master)](https://dev.azure.com/patros/OpenSource/_build/latest?definitionId=15)
========================

A collection of helpers to create HttpClient instances that automatically
handle authentication for you.

They all return HttpClient instances so your favourite extension methods will
work too.

[![NuGet](http://img.shields.io/nuget/v/Patros.AuthenticatedHttpClient.AzureAd.svg?style=flat-square)](https://www.nuget.org/packages/Patros.AuthenticatedHttpClient.AzureAd/) `Patros.AuthenticatedHttpClient.AzureAd`  
[![NuGet](http://img.shields.io/nuget/v/Patros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity.svg?style=flat-square)](https://www.nuget.org/packages/Patros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity/) `Patros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity`  
[![NuGet](http://img.shields.io/nuget/v/Patros.AuthenticatedHttpClient.Basic.svg?style=flat-square)](https://www.nuget.org/packages/Patros.AuthenticatedHttpClient.Basic/) `Patros.AuthenticatedHttpClient.Basic`

Azure AD Authenticated Http Client Example Usage
------------------------------------------------

```
dotnet add package Patros.AuthenticatedHttpClient.AzureAd
```

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

var content = await client.GetStringAsync("http://www.example.com");
```

Azure App Service Managed Identity Authenticated Http Client Example Usage
--------------------------------------------------------------------------

```
dotnet add package Patros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity
```

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new AzureAppServiceManagedIdentityAuthenticatedHttpClientOptions
{
    ResourceId = "APPLICATION ID OF THE SERVICE YOU ARE AUTHENTICATING TO (GUID)"
};

var client = AzureAppServiceManagedIdentityAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync("http://www.example.com");
```

Basic Authenticated Http Client Example Usage
---------------------------------------------

```
dotnet add package Patros.AuthenticatedHttpClient.Basic
```

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new BasicAuthenticatedHttpClientOptions
{
    UserId = "INSERT YOUR USERNAME HERE",
    Password = "INSERT YOUR PASSWORD HERE"
};

var client = BasicAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync("http://www.example.com");
```
