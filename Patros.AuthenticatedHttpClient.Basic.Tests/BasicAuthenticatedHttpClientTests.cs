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
            var parameter = BasicAuthenticatedHttpClient.GenerateAuthenticationParameter("Aladdin", "OpenSesame");

            Assert.Equal("QWxhZGRpbjpPcGVuU2VzYW1l", parameter);
        }

        [Fact]
        public void TestDefaultAuthenticationHeaderScheme()
        {
            var client = new BasicAuthenticatedHttpClient(
                new BasicAuthenticatedHttpClientOptions
                {
                    UserId = "Aladdin",
                    Password = "OpenSesame"
                }
            );

            Assert.Equal("Basic", client.DefaultRequestHeaders.Authorization.Scheme);
        }


        [Fact]
        public void TestDefaultAuthenticationHeaderParameter()
        {
            var client = new BasicAuthenticatedHttpClient(
                new BasicAuthenticatedHttpClientOptions
                {
                    UserId = "Aladdin",
                    Password = "OpenSesame"
                }
            );

            Assert.Equal("QWxhZGRpbjpPcGVuU2VzYW1l", client.DefaultRequestHeaders.Authorization.Parameter);
        }
    }
}
