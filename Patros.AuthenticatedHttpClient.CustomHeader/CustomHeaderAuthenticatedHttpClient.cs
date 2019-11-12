using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Patros.AuthenticatedHttpClient
{
    public static class CustomHeaderAuthenticatedHttpClient
    {
        public static HttpClient GetClient(CustomHeaderAuthenticatedHttpClientOptions options)
        {
            var msgHandler = new CustomHeaderAuthenticatedHttpMessageHandler(options);
            return new HttpClient(msgHandler);
        }

        public static HttpClient GetClient(CustomHeaderAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler)
        {
            var msgHandler = new CustomHeaderAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }

        public static HttpClient GetClient(MultipleCustomHeaderAuthenticatedHttpClientOptions options)
        {
            return GetClient(options, null);
        }

        public static HttpClient GetClient(MultipleCustomHeaderAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler)
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