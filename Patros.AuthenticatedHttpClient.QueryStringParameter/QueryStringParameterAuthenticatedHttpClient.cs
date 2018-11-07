using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Patros.AuthenticatedHttpClient
{
    public class QueryStringParameterAuthenticatedHttpClientOptions {
        public string Name;
        public string Value;
    }

    public class MultipleQueryStringParameterAuthenticatedHttpClientOptions {
        public Dictionary<string, string> Parameters;
    }

    public class QueryStringParameterAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private QueryStringParameterAuthenticatedHttpClientOptions _options;

        public QueryStringParameterAuthenticatedHttpMessageHandler(QueryStringParameterAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            InnerHandler = innerHandler ?? new HttpClientHandler();
            
            _options = options;

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

    public static class QueryStringParameterAuthenticatedHttpClient
    {
        public static HttpClient GetClient(QueryStringParameterAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            var msgHandler = new QueryStringParameterAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }

        public static HttpClient GetClient(MultipleQueryStringParameterAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            if (options.Parameters.Count == 0) throw new ArgumentOutOfRangeException(nameof(options), "No parameters supplied.");

            var handlers = new List<HttpMessageHandler>();
            var msgHandler = innerHandler;
            foreach (var parameter in options.Parameters)
            {
                var currentHandler = new QueryStringParameterAuthenticatedHttpMessageHandler(
                    new QueryStringParameterAuthenticatedHttpClientOptions
                    {
                        Name = parameter.Key,
                        Value = parameter.Value
                    }, 
                    msgHandler);
                
                msgHandler = currentHandler;
            }

            return new HttpClient(msgHandler);
        }
    }
}