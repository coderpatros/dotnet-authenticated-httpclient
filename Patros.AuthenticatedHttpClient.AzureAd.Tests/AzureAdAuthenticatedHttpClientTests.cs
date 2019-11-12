using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Xunit;
using Moq;
using RichardSzalay.MockHttp;
using Patros.AuthenticatedHttpClient;

namespace Patros.AuthenticatedHttpClient.AzureAd.Tests
{
    public class AzureAdAuthenticatedHttpClientTests
    {
        [Fact]
        public async Task TestRequestHasAuthorizationHeader()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
                .WithHeaders("Authorization", "Bearer test-access-token")
                .Respond(HttpStatusCode.OK);
            var mockMsgHandler = new Mock<AzureAdAuthenticatedHttpMessageHandler>(new AzureAdAuthenticatedHttpClientOptions
            {
                Tenant = "test-tenant",
                ClientId = "test-client-id",
                AppKey = "test-client-app-key",
                ResourceId = "test-resource-id"
            }, mockHttp);
            mockMsgHandler
                .Setup(handler => handler.AcquireAccessTokenAsync())
                .Returns(Task.FromResult("test-access-token"));
            mockMsgHandler.CallBase = true;
            var client = new HttpClient(mockMsgHandler.Object);

            await client.GetStringAsync("https://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task TestRequestRetriesThreeTimesToAcquireAccessToken()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
                .WithHeaders("Authorization", "Bearer test-access-token")
                .Respond(HttpStatusCode.OK);
            var mockMsgHandler = new Mock<AzureAdAuthenticatedHttpMessageHandler>(new AzureAdAuthenticatedHttpClientOptions
            {
                Tenant = "test-tenant",
                ClientId = "test-client-id",
                AppKey = "test-client-app-key",
                ResourceId = "test-resource-id"
            }, mockHttp);
            mockMsgHandler
                .SetupSequence(handler => handler.AcquireAccessTokenAsync())
                .Throws(new AdalException("temporarily_unavailable"))
                .Throws(new AdalException("temporarily_unavailable"))
                .Returns(Task.FromResult("test-access-token"));
            mockMsgHandler.CallBase = true;
            var client = new HttpClient(mockMsgHandler.Object);

            await client.GetStringAsync("https://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }    

        [Fact]
        public async Task TestRequestFailsOnRepeatedFailuresToAcquireAccessTokenFailure()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Fallback.Respond(req => new HttpResponseMessage(HttpStatusCode.Unauthorized));
            var mockMsgHandler = new Mock<AzureAdAuthenticatedHttpMessageHandler>(new AzureAdAuthenticatedHttpClientOptions
            {
                Tenant = "test-tenant",
                ClientId = "test-client-id",
                AppKey = "test-client-app-key",
                ResourceId = "test-resource-id"
            }, mockHttp);
            mockMsgHandler
                .SetupSequence(handler => handler.AcquireAccessTokenAsync())
                .Throws(new AdalException("temporarily_unavailable"))
                .Throws(new AdalException("temporarily_unavailable"))
                .Throws(new AdalException("temporarily_unavailable"));
            mockMsgHandler.CallBase = true;
            var client = new HttpClient(mockMsgHandler.Object);

            var exc = await Record.ExceptionAsync(async () => await client.GetStringAsync("https://www.example.com"));

            Assert.IsType<HttpRequestException>(exc);
        }    
    }
}
