using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using RichardSzalay.MockHttp;
using Patros.AuthenticatedHttpClient;

namespace Patros.AuthenticatedHttpClient.CustomHeader.Tests
{
    public class CustomHeaderAuthenticatedHttpClientTests
    {
        [Fact]
        public async Task TestRequestAddsAuthenticationHeader()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com")
                .WithHeaders("test-name", "test-value")
                .Respond(HttpStatusCode.OK);
            var client = CustomHeaderAuthenticatedHttpClient.GetClient(new CustomHeaderAuthenticatedHttpClientOptions
            {
                Name = "test-name",
                Value = "test-value"
            }, mockHttp);

            var responseContent = await client.GetStringAsync("http://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestMultipleParameterRequestAddsAuthenticationParameters()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com")
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

            var responseContent = await client.GetStringAsync("http://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestMultipleParameterWithSingleParameterRequestAddsAuthenticationParameter()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com")
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

            var responseContent = await client.GetStringAsync("http://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
