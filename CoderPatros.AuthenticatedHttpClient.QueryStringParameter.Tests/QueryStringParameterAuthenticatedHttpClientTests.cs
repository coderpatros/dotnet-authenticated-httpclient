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
            using (var mockHttp = new MockHttpMessageHandler())
            {
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

                await client.GetStringAsync(new Uri("https://www.example.com")).ConfigureAwait(false);

                mockHttp.VerifyNoOutstandingExpectation();
            }
        }

        [Fact]
        public async Task TestRequestAddsAuthenticationParameterWithEmptyQuery()
        {
            using (var mockHttp = new MockHttpMessageHandler())
            {
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

                await client.GetStringAsync(new Uri("https://www.example.com?")).ConfigureAwait(false);

                mockHttp.VerifyNoOutstandingExpectation();
            }
        }

        [Fact]
        public async Task TestRequestReplacesAuthenticationParameter()
        {
            using (var mockHttp = new MockHttpMessageHandler())
            {
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

                await client.GetStringAsync(new Uri("https://www.example.com?test-name=incorrect-value")).ConfigureAwait(false);

                mockHttp.VerifyNoOutstandingExpectation();
            }
        }

        [Fact]
        public async Task TestRequestAddsAuthenticationParameterToExistingQuery()
        {
            using (var mockHttp = new MockHttpMessageHandler())
            {
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

                await client.GetStringAsync(new Uri("https://www.example.com?other-name=other-value")).ConfigureAwait(false);

                mockHttp.VerifyNoOutstandingExpectation();
            }
        }

        [Fact]
        public async Task TestMultipleParameterRequestAddsAuthenticationParameters()
        {
            using (var mockHttp = new MockHttpMessageHandler())
            {
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

                await client.GetStringAsync(new Uri("https://www.example.com")).ConfigureAwait(false);

                mockHttp.VerifyNoOutstandingExpectation();
            }
        }

        [Fact]
        public async Task TestMultipleParameterWithSingleParameterRequestAddsAuthenticationParameter()
        {
            using (var mockHttp = new MockHttpMessageHandler())
            {
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

                await client.GetStringAsync(new Uri("https://www.example.com")).ConfigureAwait(false);

                mockHttp.VerifyNoOutstandingExpectation();
            }
        }
    }
}
