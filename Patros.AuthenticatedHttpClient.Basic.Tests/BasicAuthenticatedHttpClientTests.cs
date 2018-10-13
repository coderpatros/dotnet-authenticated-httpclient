using System;
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
    }
}
