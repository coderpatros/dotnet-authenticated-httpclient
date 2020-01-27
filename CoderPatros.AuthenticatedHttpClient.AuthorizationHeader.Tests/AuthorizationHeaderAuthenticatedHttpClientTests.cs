using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using RichardSzalay.MockHttp;
using CoderPatros.AuthenticatedHttpClient;

namespace CoderPatros.AuthenticatedHttpClient.AuthorizationHeader.Tests
{
    public class AuthorizationHeaderAuthenticatedHttpClientTests
    {
        [Fact]
        public async Task TestRequestAddsAuthenticationHeader()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
                .WithHeaders("Authorization", "test-value")
                .Respond(HttpStatusCode.OK);
            var client = AuthorizationHeaderAuthenticatedHttpClient.GetClient(new AuthorizationHeaderAuthenticatedHttpClientOptions
            {
                Value = "test-value"
            }, mockHttp);

            await client.GetStringAsync("https://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
