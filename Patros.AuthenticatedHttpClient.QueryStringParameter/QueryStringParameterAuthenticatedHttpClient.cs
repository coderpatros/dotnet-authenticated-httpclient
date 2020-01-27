using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CoderPatros.AuthenticatedHttpClient
{
    public static class QueryStringParameterAuthenticatedHttpClient
    {
        public static HttpClient GetClient(QueryStringParameterAuthenticatedHttpClientOptions options)
        {
            var msgHandler = new QueryStringParameterAuthenticatedHttpMessageHandler(options);
            return new HttpClient(msgHandler);
        }

        public static HttpClient GetClient(QueryStringParameterAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler)
        {
            var msgHandler = new QueryStringParameterAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }

        public static HttpClient GetClient(MultipleQueryStringParameterAuthenticatedHttpClientOptions options)
        {
            return GetClient(options, null);
        }

        public static HttpClient GetClient(MultipleQueryStringParameterAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler)
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