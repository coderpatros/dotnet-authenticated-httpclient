using System;
using Xunit;
using Patros.AuthenticatedHttpClient;

namespace Patros.AuthenticatedHttpClient.Basic.Tests
{
    // test values taken verbatim from RFC 1945
    public class BasicAuthenticatedHttpClientTests
    {
        [Fact]
        public void TestGenerateAuthenticationParameter()
        {
            var parameter = BasicAuthenticatedHttpMessageHandler.GenerateAuthenticationParameter("Aladdin", "OpenSesame");

            Assert.Equal("QWxhZGRpbjpPcGVuU2VzYW1l", parameter);
        }
    }
}
