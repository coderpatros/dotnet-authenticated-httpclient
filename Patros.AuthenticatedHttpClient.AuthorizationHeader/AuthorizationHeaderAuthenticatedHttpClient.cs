using System.Net.Http;

namespace Patros.AuthenticatedHttpClient
{
    public static class AuthorizationHeaderAuthenticatedHttpClient
    {
        public static HttpClient GetClient(AuthorizationHeaderAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            var msgHandler = new AuthorizationHeaderAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }
    }
}