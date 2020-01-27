using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CoderPatros.AuthenticatedHttpClient
{
    public class QueryStringParameterAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private readonly QueryStringParameterAuthenticatedHttpClientOptions _options;

        public QueryStringParameterAuthenticatedHttpMessageHandler(
            QueryStringParameterAuthenticatedHttpClientOptions options)
        {
            _options = options;
        }

        public QueryStringParameterAuthenticatedHttpMessageHandler(
            QueryStringParameterAuthenticatedHttpClientOptions options,
            HttpMessageHandler innerHandler) : this(options)
        {
            InnerHandler = innerHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var escapedValue = Uri.EscapeDataString(_options.Value);
            var authenticatedUri = new UriBuilder(request.RequestUri);
            if (request.RequestUri.Query == string.Empty || request.RequestUri.Query == "?")
            {
                authenticatedUri.Query = $"?{_options.Name}={escapedValue}";
            }
            else
            {
                var queryParameters = HttpUtility.ParseQueryString(request.RequestUri.Query);
                queryParameters[_options.Name] = escapedValue;
                authenticatedUri.Query = queryParameters.ToString();
            }
            request.RequestUri = authenticatedUri.Uri;

            return await base.SendAsync(request, cancellationToken);
        }
    }
}