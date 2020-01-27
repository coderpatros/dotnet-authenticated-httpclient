using System.Net.Http;

namespace CoderPatros.AuthenticatedHttpClient
{
    public static class AzureAppServiceManagedIdentityAuthenticatedHttpClient
    {
       public static HttpClient GetClient(AzureAppServiceManagedIdentityAuthenticatedHttpClientOptions options)
        {
            var msgHandler = new AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler(options);
            return new HttpClient(msgHandler);
        }

       public static HttpClient GetClient(AzureAppServiceManagedIdentityAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler)
        {
            var msgHandler = new AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }
     }
}