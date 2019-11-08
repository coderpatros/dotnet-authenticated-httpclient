using System.Net.Http;

namespace Patros.AuthenticatedHttpClient
{
    public static class BasicAuthenticatedHttpClient
    {
        public static HttpClient GetClient(BasicAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            var msgHandler = new BasicAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }
    }
}