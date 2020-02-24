using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using RichardSzalay.MockHttp;

namespace CoderPatros.AuthenticatedHttpClient.Basic.Tests
{
    public class BasicAuthenticatedHttpClientTests
    {
        [Fact]
        public void TestGenerateAuthenticationParameterRFC1945()
        {
            // test value taken verbatim from RFC 1945
            var parameter = BasicAuthenticatedHttpMessageHandler.GenerateAuthenticationParameter("Aladdin", "OpenSesame");

            Assert.Equal("QWxhZGRpbjpPcGVuU2VzYW1l", parameter);
        }

        [Fact]
        public void TestGenerateAuthenticationParameterRFC7617()
        {
            // test value taken verbatim from RFC 7617
            var parameter = BasicAuthenticatedHttpMessageHandler.GenerateAuthenticationParameter("Aladdin", "open sesame");

            Assert.Equal("QWxhZGRpbjpvcGVuIHNlc2FtZQ==", parameter);
        }

        [Fact]
        public async Task TestRequestHasAuthorizationHeader()
        {
            using (var mockHttp = new MockHttpMessageHandler())
            {
                mockHttp
                    .Expect("https://www.example.com")
                    .WithHeaders("Authorization", "Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==")
                    .Respond(HttpStatusCode.OK);
                var client = BasicAuthenticatedHttpClient.GetClient(new BasicAuthenticatedHttpClientOptions
                {
                    UserId = "Aladdin",
                    Password = "open sesame"
                }, mockHttp);

                await client.GetStringAsync(new Uri("https://www.example.com")).ConfigureAwait(false);

                mockHttp.VerifyNoOutstandingExpectation();
            }
        }
    }
}
