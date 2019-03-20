Authenticated HttpClient [![Build status](https://dev.azure.com/patros/OpenSource/_apis/build/status/Patros.AuthenticatedHttpClient?branchName=master)](https://dev.azure.com/patros/OpenSource/_build/latest?definitionId=15)
========================

A collection of helpers to create HttpClient instances that automatically
handle authentication for you.

They all return HttpClient instances so your favourite extension methods will
work too.

[![NuGet](https://img.shields.io/nuget/v/Patros.AuthenticatedHttpClient.AuthorizationHeader.svg?style=flat-square)](https://www.nuget.org/packages/Patros.AuthenticatedHttpClient.AuthorizationHeader/) `Patros.AuthenticatedHttpClient.AuthorizationHeader`  
[![NuGet](https://img.shields.io/nuget/v/Patros.AuthenticatedHttpClient.AzureAd.svg?style=flat-square)](https://www.nuget.org/packages/Patros.AuthenticatedHttpClient.AzureAd/) `Patros.AuthenticatedHttpClient.AzureAd`  
[![NuGet](https://img.shields.io/nuget/v/Patros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity.svg?style=flat-square)](https://www.nuget.org/packages/Patros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity/) `Patros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity`  
[![NuGet](https://img.shields.io/nuget/v/Patros.AuthenticatedHttpClient.Basic.svg?style=flat-square)](https://www.nuget.org/packages/Patros.AuthenticatedHttpClient.Basic/) `Patros.AuthenticatedHttpClient.Basic`  
[![NuGet](https://img.shields.io/nuget/v/Patros.AuthenticatedHttpClient.CustomHeader.svg?style=flat-square)](https://www.nuget.org/packages/Patros.AuthenticatedHttpClient.CustomHeader/) `Patros.AuthenticatedHttpClient.CustomHeader`  
[![NuGet](https://img.shields.io/nuget/v/Patros.AuthenticatedHttpClient.QueryStringParameter.svg?style=flat-square)](https://www.nuget.org/packages/Patros.AuthenticatedHttpClient.QueryStringParameter/) `Patros.AuthenticatedHttpClient.QueryStringParameter`  

Authorization Header Authenticated Http Client Example Usage
------------------------------------------------------------

```
dotnet add package Patros.AuthenticatedHttpClient.AuthorizationHeader
```

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new AuthorizationHeaderAuthenticatedHttpClientOptions
{
    Value = "INSERT YOUR AUTHORIZATION HEADER HERE"
};

var client = AuthorizationHeaderAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync("https://www.example.com");
```

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

var content = await client.GetStringAsync("https://www.example.com");
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

var content = await client.GetStringAsync("https://www.example.com");
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

var content = await client.GetStringAsync("https://www.example.com");
```

Custom Header Authenticated Http Client Example Usage
-----------------------------------------------------

```
dotnet add package Patros.AuthenticatedHttpClient.CustomHeader
```

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new CustomHeaderAuthenticatedHttpClientOptions
{
    Name = "INSERT NAME OF CUSTOM HEADER",
    Value = "INSERT VALUE OF CUSTOM HEADER"
};

var client = CustomHeaderAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync("https://www.example.com");
```

Or if multiple custom headers are required...

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new MultipleCustomHeaderAuthenticatedHttpClientOptions
{
    Parameters = new Dictionary<string, string>
    {
        { "NAME OF 1ST HEADER", "VALUE OF 1ST HEADER" },
        { "NAME OF 2ND HEADER", "VALUE OF 2ND HEADER" },
...
        { "NAME OF NTH HEADER", "VALUE OF NTH HEADER" }
    }
};

var client = CustomHeaderAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync("https://www.example.com");
```

Query String Parameter Authenticated Http Client Example Usage
--------------------------------------------------------------

```
dotnet add package Patros.AuthenticatedHttpClient.QueryStringParameter
```

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new QueryStringParameterAuthenticatedHttpClientOptions
{
    Name = "INSERT NAME OF QUERY STRING PARAMETER",
    Value = "INSERT VALUE OF QUERY STRING PARAMETER"
};

var client = QueryStringParameterAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync("https://www.example.com");
```

Or if multiple parameters are required...

```csharp
using Patros.AuthenticatedHttpClient;

...

var options = new MultipleQueryStringParameterAuthenticatedHttpClientOptions
{
    Parameters = new Dictionary<string, string>
    {
        { "NAME OF 1ST PARAMETER", "VALUE OF 1ST PARAMETER" },
        { "NAME OF 2ND PARAMETER", "VALUE OF 2ND PARAMETER" },
...
        { "NAME OF NTH PARAMETER", "VALUE OF NTH PARAMETER" }
    }
};

var client = QueryStringParameterAuthenticatedHttpClient.GetClient(options);

var content = await client.GetStringAsync("https://www.example.com");
```

Chaining Multiple Authentication Methods
----------------------------------------

Let's just pretend that you need to interact with an API that requires
multiple authentication methods. Just for an example, let's pretend it
requires basic authentication, a query string parameter and a custom HTTP
header. I know, that would never happen in the real world... oh wait... there's the [Whispir API](https://whispir.github.io/api/).

```
dotnet add package Patros.AuthenticatedHttpClient.Basic
dotnet add package Patros.AuthenticatedHttpClient.CustomHeader
dotnet add package Patros.AuthenticatedHttpClient.QueryStringParameter
```

```csharp
using Patros.AuthenticatedHttpClient;

...

var basicOptions = new BasicAuthenticatedHttpClientOptions
{
    UserId = "WhispirUsername",
    Password = "WhispirPassword"
};
var basicClient = BasicAuthenticatedHttpClient.GetClient(basicOptions);

var customHeaderOptions = new CustomHeaderAuthenticatedHttpClientOptions
{
    Name = "x-api-key",
    Value = "WhispirApiKey"
};
var customHeaderClient = CustomHeaderAuthenticatedHttpClient.GetClient(customHeaderOptions, basicClient);

var queryStringOptions = new QueryStringParameterAuthenticatedHttpClientOptions
{
    Name = "apikey",
    Value = "WhispirApiKey" // yes, the same one as above :|
};
var client = QueryStringParameterAuthenticatedHttpClient.GetClient(queryStringOptions, customHeaderClient);

var content = await client.GetStringAsync("https://api.<region>.whispir.com/messages");
```

To make the above simpler I made a `WhispirApiHttpClient` class that is available in my [dotnet-whispir-api](https://github.com/patros/dotnet-whispir-api) project.
