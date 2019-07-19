using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Patros.AuthenticatedHttpClient
{
    public class CustomHeaderAuthenticatedHttpClientOptions {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class MultipleCustomHeaderAuthenticatedHttpClientOptions {
        public Dictionary<string, string> Headers;
    }

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

    public static class CustomHeaderAuthenticatedHttpClient
    {
        public static HttpClient GetClient(CustomHeaderAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            var msgHandler = new CustomHeaderAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }

        public static HttpClient GetClient(MultipleCustomHeaderAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            if (options.Headers.Count == 0) throw new ArgumentOutOfRangeException(nameof(options), "No headers supplied.");

            var handlers = new List<HttpMessageHandler>();
            var msgHandler = innerHandler;
            foreach (var header in options.Headers)
            {
                var currentHandler = new CustomHeaderAuthenticatedHttpMessageHandler(
                    new CustomHeaderAuthenticatedHttpClientOptions
                    {
                        Name = header.Key,
                        Value = header.Value
                    }, 
                    msgHandler);
                
                msgHandler = currentHandler;
            }

            return new HttpClient(msgHandler);   
        }
    }
}