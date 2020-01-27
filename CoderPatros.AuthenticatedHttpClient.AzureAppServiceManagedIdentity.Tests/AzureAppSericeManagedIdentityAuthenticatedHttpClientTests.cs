using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Moq;
using RichardSzalay.MockHttp;
using CoderPatros.AuthenticatedHttpClient;

namespace CoderPatros.AuthenticatedHttpClient.AzureAppServiceManagedIdentity.Tests
{
    public class AzureAppServiceManagedIdentityAuthenticatedHttpClientTests
    {
        [Fact]
        public async Task TestRequestHasAuthorizationHeader()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("https://www.example.com")
                .WithHeaders("Authorization", "Bearer test-access-token")
                .Respond(HttpStatusCode.OK);
            var mockMsgHandler = new Mock<AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler>(new AzureAppServiceManagedIdentityAuthenticatedHttpClientOptions
            {
                ResourceId = "test-resource-id"
            }, mockHttp);
            mockMsgHandler
                .Setup(handler => handler.GetAccessTokenAsync())
                .Returns(Task.FromResult("test-access-token"));
            mockMsgHandler.CallBase = true;
            var client = new HttpClient(mockMsgHandler.Object);

            await client.GetStringAsync("https://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
