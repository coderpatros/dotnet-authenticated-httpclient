using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using RichardSzalay.MockHttp;
using Patros.AuthenticatedHttpClient;

namespace Patros.AuthenticatedHttpClient.QueryStringParameter.Tests
{
    public class QueryStringParameterAuthenticatedHttpClientTests
    {
        [Fact]
        public async Task TestRequestAddsAuthenticationParameter()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com")
                .WithQueryString(new Dictionary<string, string> {
                    { "test-name", "test-value" }
                })
                .Respond(HttpStatusCode.OK);
            var client = QueryStringParameterAuthenticatedHttpClient.GetClient(new QueryStringParameterAuthenticatedHttpClientOptions
            {
                Name = "test-name",
                Value = "test-value"
            }, mockHttp);

            var responseContent = await client.GetStringAsync("http://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestRequestAddsAuthenticationParameterWithEmptyQuery()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com")
                .WithQueryString(new Dictionary<string, string> {
                    { "test-name", "test-value" }
                })
                .Respond(HttpStatusCode.OK);
            var client = QueryStringParameterAuthenticatedHttpClient.GetClient(new QueryStringParameterAuthenticatedHttpClientOptions
            {
                Name = "test-name",
                Value = "test-value"
            }, mockHttp);

            var responseContent = await client.GetStringAsync("http://www.example.com?");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestRequestReplacesAuthenticationParameter()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com")
                .WithQueryString(new Dictionary<string, string> {
                    { "test-name", "test-value" }
                })
                .Respond(HttpStatusCode.OK);
            var client = QueryStringParameterAuthenticatedHttpClient.GetClient(new QueryStringParameterAuthenticatedHttpClientOptions
            {
                Name = "test-name",
                Value = "test-value"
            }, mockHttp);

            var responseContent = await client.GetStringAsync("http://www.example.com?test-name=incorrect-value");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestRequestAddsAuthenticationParameterToExistingQuery()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com")
                .WithQueryString(new Dictionary<string, string> {
                    { "test-name", "test-value" },
                    { "other-name", "other-value" }
                })
                .Respond(HttpStatusCode.OK);
            var client = QueryStringParameterAuthenticatedHttpClient.GetClient(new QueryStringParameterAuthenticatedHttpClientOptions
            {
                Name = "test-name",
                Value = "test-value"
            }, mockHttp);

            var responseContent = await client.GetStringAsync("http://www.example.com?other-name=other-value");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestMultipleParameterRequestAddsAuthenticationParameters()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com")
                .WithQueryString(new Dictionary<string, string> {
                    { "test-name-1", "test-value-1" },
                    { "test-name-2", "test-value-2" }
                })
                .Respond(HttpStatusCode.OK);
            var client = QueryStringParameterAuthenticatedHttpClient.GetClient(new MultipleQueryStringParameterAuthenticatedHttpClientOptions
            {
                Parameters = new Dictionary<string, string>
                {
                    { "test-name-1", "test-value-1"},
                    { "test-name-2", "test-value-2"}
                }
            }, mockHttp);

            var responseContent = await client.GetStringAsync("http://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestMultipleParameterWithSingleParameterRequestAddsAuthenticationParameter()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com")
                .WithQueryString(new Dictionary<string, string> {
                    { "test-name", "test-value" }
                })
                .Respond(HttpStatusCode.OK);
            var client = QueryStringParameterAuthenticatedHttpClient.GetClient(new MultipleQueryStringParameterAuthenticatedHttpClientOptions
            {
                Parameters = new Dictionary<string, string>
                {
                    { "test-name", "test-value"}
                }
            }, mockHttp);

            var responseContent = await client.GetStringAsync("http://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
