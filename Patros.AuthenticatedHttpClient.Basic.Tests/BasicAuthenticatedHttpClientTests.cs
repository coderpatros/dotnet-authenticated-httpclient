using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Patros.AuthenticatedHttpClient;

namespace Patros.AuthenticatedHttpClient.Basic.Tests
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
            var client = BasicAuthenticatedHttpClient.GetClient(new BasicAuthenticatedHttpClientOptions
            {
                UserId = "Aladdin",
                Password = "open sesame"
            });

            var responseContent = await client.GetStringAsync("https://postman-echo.com/get");
            dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

            Assert.Equal("Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==", (string)responseObject.headers.authorization);
        }
    }
}
