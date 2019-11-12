using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Patros.AuthenticatedHttpClient
{
    public class AuthorizationHeaderAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private readonly AuthenticationHeaderValue _authorizationHeader;

        public AuthorizationHeaderAuthenticatedHttpMessageHandler(AuthorizationHeaderAuthenticatedHttpClientOptions options)
        {
            _authorizationHeader = new AuthenticationHeaderValue(options.Value);
        }

        public AuthorizationHeaderAuthenticatedHttpMessageHandler(
            AuthorizationHeaderAuthenticatedHttpClientOptions options, 
            HttpMessageHandler innerHandler) : this(options)
        {
            InnerHandler = innerHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = _authorizationHeader;
            return await base.SendAsync(request, cancellationToken);
        }
    }
}