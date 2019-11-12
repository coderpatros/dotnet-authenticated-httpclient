using System.Net.Http;

namespace Patros.AuthenticatedHttpClient
{
    public static class BasicAuthenticatedHttpClient
    {
        public static HttpClient GetClient(BasicAuthenticatedHttpClientOptions options)
        {
            var msgHandler = new BasicAuthenticatedHttpMessageHandler(options);
            return new HttpClient(msgHandler);
        }

        public static HttpClient GetClient(BasicAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler)
        {
            var msgHandler = new BasicAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }
    }
}