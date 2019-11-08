using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Patros.AuthenticatedHttpClient
{
    public class CustomHeaderAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private CustomHeaderAuthenticatedHttpClientOptions _options;

        public CustomHeaderAuthenticatedHttpMessageHandler(CustomHeaderAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            InnerHandler = innerHandler ?? new HttpClientHandler();
            
            _options = options;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Remove(_options.Name);
            request.Headers.Add(_options.Name, _options.Value);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}