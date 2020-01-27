using System.Net.Http;

namespace CoderPatros.AuthenticatedHttpClient
{
    public static class AuthorizationHeaderAuthenticatedHttpClient
    {
        public static HttpClient GetClient(AuthorizationHeaderAuthenticatedHttpClientOptions options)
        {
            var msgHandler = new AuthorizationHeaderAuthenticatedHttpMessageHandler(options);
            return new HttpClient(msgHandler);
        }

        public static HttpClient GetClient(AuthorizationHeaderAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler)
        {
            var msgHandler = new AuthorizationHeaderAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }
    }
}