using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using RichardSzalay.MockHttp;

namespace CoderPatros.AuthenticatedHttpClient.QueryStringParameter.Tests
{
    public class QueryStringParameterAuthenticatedHttpClientTests
    {
        [Fact]
        public async Task TestRequestAddsAuthenticationParameter()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
                .WithQueryString(new Dictionary<string, string> {
                    { "test-name", "test-value" }
                })
                .Respond(HttpStatusCode.OK);
            var client = QueryStringParameterAuthenticatedHttpClient.GetClient(new QueryStringParameterAuthenticatedHttpClientOptions
            {
                Name = "test-name",
                Value = "test-value"
            }, mockHttp);

            await client.GetStringAsync("https://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestRequestAddsAuthenticationParameterWithEmptyQuery()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
                .WithQueryString(new Dictionary<string, string> {
                    { "test-name", "test-value" }
                })
                .Respond(HttpStatusCode.OK);
            var client = QueryStringParameterAuthenticatedHttpClient.GetClient(new QueryStringParameterAuthenticatedHttpClientOptions
            {
                Name = "test-name",
                Value = "test-value"
            }, mockHttp);

            await client.GetStringAsync("https://www.example.com?");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestRequestReplacesAuthenticationParameter()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
                .WithQueryString(new Dictionary<string, string> {
                    { "test-name", "test-value" }
                })
                .Respond(HttpStatusCode.OK);
            var client = QueryStringParameterAuthenticatedHttpClient.GetClient(new QueryStringParameterAuthenticatedHttpClientOptions
            {
                Name = "test-name",
                Value = "test-value"
            }, mockHttp);

            await client.GetStringAsync("https://www.example.com?test-name=incorrect-value");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestRequestAddsAuthenticationParameterToExistingQuery()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
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

            await client.GetStringAsync("https://www.example.com?other-name=other-value");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestMultipleParameterRequestAddsAuthenticationParameters()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
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

            await client.GetStringAsync("https://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestMultipleParameterWithSingleParameterRequestAddsAuthenticationParameter()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
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

            await client.GetStringAsync("https://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
