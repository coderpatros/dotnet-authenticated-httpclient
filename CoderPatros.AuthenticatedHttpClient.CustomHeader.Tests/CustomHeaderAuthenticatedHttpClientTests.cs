using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using RichardSzalay.MockHttp;

namespace CoderPatros.AuthenticatedHttpClient.CustomHeader.Tests
{
    public class CustomHeaderAuthenticatedHttpClientTests
    {
        [Fact]
        public async Task TestRequestAddsAuthenticationHeader()
        {
            using (var mockHttp = new MockHttpMessageHandler())
            {
                mockHttp
                    .Expect("https://www.example.com")
                    .WithHeaders("test-name", "test-value")
                    .Respond(HttpStatusCode.OK);
                var client = CustomHeaderAuthenticatedHttpClient.GetClient(new CustomHeaderAuthenticatedHttpClientOptions
                {
                    Name = "test-name",
                    Value = "test-value"
                }, mockHttp);

                await client.GetStringAsync(new Uri("https://www.example.com")).ConfigureAwait(false);

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
                    .WithHeaders(new Dictionary<string, string> {
                        { "test-name-1", "test-value-1" },
                        { "test-name-2", "test-value-2" }
                    })
                    .Respond(HttpStatusCode.OK);
                var client = CustomHeaderAuthenticatedHttpClient.GetClient(new MultipleCustomHeaderAuthenticatedHttpClientOptions
                {
                    Headers = new Dictionary<string, string>
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
                    .WithHeaders(new Dictionary<string, string> {
                        { "test-name-1", "test-value-1" }
                    })
                    .Respond(HttpStatusCode.OK);
                var client = CustomHeaderAuthenticatedHttpClient.GetClient(new MultipleCustomHeaderAuthenticatedHttpClientOptions
                {
                    Headers = new Dictionary<string, string>
                    {
                        { "test-name-1", "test-value-1"}
                    }
                }, mockHttp);

                await client.GetStringAsync(new Uri("https://www.example.com")).ConfigureAwait(false);

                mockHttp.VerifyNoOutstandingExpectation();
            }
        }
    }
}
