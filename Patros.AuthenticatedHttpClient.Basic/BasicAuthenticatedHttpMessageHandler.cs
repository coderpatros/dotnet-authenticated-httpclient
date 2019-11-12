using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Patros.AuthenticatedHttpClient
{
    public class BasicAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private readonly AuthenticationHeaderValue _authorizationHeader;

        public BasicAuthenticatedHttpMessageHandler(
            BasicAuthenticatedHttpClientOptions options)
        {
            _authorizationHeader = new AuthenticationHeaderValue(
                "Basic", 
                BasicAuthenticatedHttpMessageHandler.GenerateAuthenticationParameter(options.UserId, options.Password));
        }

        public BasicAuthenticatedHttpMessageHandler(
            BasicAuthenticatedHttpClientOptions options,
            HttpMessageHandler innerHandler) : this(options)
        {
            InnerHandler = innerHandler;
        }

        internal static string GenerateAuthenticationParameter(string userId, string password)
        {
            // implemented as per RFC 7617 https://tools.ietf.org/html/rfc7617.html
            var userPass = string.Format("{0}:{1}", userId, password);
            var userPassBytes = System.Text.Encoding.UTF8.GetBytes(userPass);
            var userPassBase64 = System.Convert.ToBase64String(userPassBytes);
            return userPassBase64;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = _authorizationHeader;
            return await base.SendAsync(request, cancellationToken);
        }
    }
}