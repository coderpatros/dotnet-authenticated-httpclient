using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Patros.AuthenticatedHttpClient
{
    public class AuthorizationHeaderAuthenticatedHttpClientOptions
    {
        public string Value;
    }

    public class AuthorizationHeaderAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private AuthenticationHeaderValue _authorizationHeader;

        public AuthorizationHeaderAuthenticatedHttpMessageHandler(AuthorizationHeaderAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            InnerHandler = innerHandler ?? new HttpClientHandler();
            
            _authorizationHeader = new AuthenticationHeaderValue(options.Value);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = _authorizationHeader;
            return await base.SendAsync(request, cancellationToken);
        }
    }

    public static class AuthorizationHeaderAuthenticatedHttpClient
    {
        public static HttpClient GetClient(AuthorizationHeaderAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            var msgHandler = new AuthorizationHeaderAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }
    }
}