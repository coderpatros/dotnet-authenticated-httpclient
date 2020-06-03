[![Build Status](https://github.com/coderpatros/dotnet-authenticated-httpclient/workflows/.NET%20Core%20CI/badge.svg)](https://github.com/coderpatros/dotnet-authenticated-httpclient/actions?workflow=.NET+Core+CI)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
![Twitter Follow](https://img.shields.io/twitter/follow/coderpatros?style=social)

Authenticated HttpClient
========================

A collection of helpers to create HttpClient instances that automatically
handle authentication for you.

They all return HttpClient instances so your favourite extension methods will
work too.

[![NuGet](https://img.shields.io/nuget/v/CoderPatros.AuthenticatedHttpClient.AuthorizationHeader.svg?style=flat-square)](https://www.nuget.org/packages/CoderPatros.AuthenticatedHttpClient.AuthorizationHeader/)
![Nuget](https://img.shields.io/nuget/dt/CoderPatros.AuthenticatedHttpClient.AuthorizationHeader.svg)
`CoderPatros.AuthenticatedHttpClient.AuthorizationHeader`  
[![NuGet](https://img.shields.io/nuget/v/CoderPatros.AuthenticatedHttpClient.AzureAd.svg?style=flat-square)](https://www.nuget.org/packages/CoderPatros.AuthenticatedHttpClient.AzureAd/)
![Nuget](https://img.shields.io/nuget/dt/CoderPatros.AuthenticatedHttpClient.AzureAd.svg)
`CoderPatros.AuthenticatedHttpClient.AzureAd`  
[![NuGet](https://img.shields.io/nuget/v/CoderPatros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity.svg?style=flat-square)](https://www.nuget.org/packages/CoderPatros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity/)
![Nuget](https://img.shields.io/nuget/dt/CoderPatros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity.svg)
`CoderPatros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity`  
[![NuGet](https://img.shields.io/nuget/v/CoderPatros.AuthenticatedHttpClient.Basic.svg?style=flat-square)](https://www.nuget.org/packages/CoderPatros.AuthenticatedHttpClient.Basic/)
![Nuget](https://img.shields.io/nuget/dt/CoderPatros.AuthenticatedHttpClient.Basic.svg)
`CoderPatros.AuthenticatedHttpClient.Basic`  
[![NuGet](https://img.shields.io/nuget/v/CoderPatros.AuthenticatedHttpClient.CustomHeader.svg?style=flat-square)](https://www.nuget.org/packages/CoderPatros.AuthenticatedHttpClient.CustomHeader/)
![Nuget](https://img.shields.io/nuget/dt/CoderPatros.AuthenticatedHttpClient.CustomHeader.svg)
`CoderPatros.AuthenticatedHttpClient.CustomHeader`  
[![NuGet](https://img.shields.io/nuget/v/CoderPatros.AuthenticatedHttpClient.QueryStringParameter.svg?style=flat-square)](https://www.nuget.org/packages/CoderPatros.AuthenticatedHttpClient.QueryStringParameter/)
![Nuget](https://img.shields.io/nuget/dt/CoderPatros.AuthenticatedHttpClient.QueryStringParameter.svg)
`CoderPatros.AuthenticatedHttpClient.QueryStringParameter`  

Authorization Header Authenticated Http Client Example Usage
------------------------------------------------------------

```shell
dotnet add package CoderPatros.AuthenticatedHttpClient.AuthorizationHeader
```

```csharp
using CoderPatros.AuthenticatedHttpClient;

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
dotnet add package CoderPatros.AuthenticatedHttpClient.AzureAd
```

```csharp
using CoderPatros.AuthenticatedHttpClient;

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

```shell
dotnet add package CoderPatros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity
```

```csharp
using CoderPatros.AuthenticatedHttpClient;

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

```shell
dotnet add package CoderPatros.AuthenticatedHttpClient.Basic
```

```csharp
using CoderPatros.AuthenticatedHttpClient;

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

```shell
dotnet add package CoderPatros.AuthenticatedHttpClient.CustomHeader
```

```csharp
using CoderPatros.AuthenticatedHttpClient;

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
using CoderPatros.AuthenticatedHttpClient;

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

```shell
dotnet add package CoderPatros.AuthenticatedHttpClient.QueryStringParameter
```

```csharp
using CoderPatros.AuthenticatedHttpClient;

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
using CoderPatros.AuthenticatedHttpClient;

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

```shell
dotnet add package CoderPatros.AuthenticatedHttpClient.Basic
dotnet add package CoderPatros.AuthenticatedHttpClient.CustomHeader
dotnet add package CoderPatros.AuthenticatedHttpClient.QueryStringParameter
```

```csharp
using CoderPatros.AuthenticatedHttpClient;

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

To make the above simpler I made a `WhispirApiHttpClient` class that is available in my [dotnet-whispir-api](https://github.com/coderpatros/dotnet-whispir-api) project.
