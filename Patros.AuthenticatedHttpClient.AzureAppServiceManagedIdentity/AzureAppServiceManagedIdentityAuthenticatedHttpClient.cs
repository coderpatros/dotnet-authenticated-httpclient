using System.Net.Http;

namespace Patros.AuthenticatedHttpClient
{
    public class AzureAppServiceManagedIdentityAuthenticatedHttpClient
    {
       public static HttpClient GetClient(AzureAppServiceManagedIdentityAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            var msgHandler = new AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }
     }
}