using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Patros.AuthenticatedHttpClient
{
    public class BasicAuthenticatedHttpClientOptions {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class BasicAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private AuthenticationHeaderValue _authorizationHeader;

        public BasicAuthenticatedHttpMessageHandler(BasicAuthenticatedHttpClientOptions options)
        {
            _authorizationHeader = new AuthenticationHeaderValue(
                "Basic", 
                BasicAuthenticatedHttpMessageHandler.GenerateAuthenticationParameter(options.UserId, options.Password));
        }

        internal static string GenerateAuthenticationParameter(string userId, string password)
        {
            // implemented as per RFC 1945 https://tools.ietf.org/html/rfc1945
            var userPass = string.Format("{0}:{1}", userId, password);
            var userPassBytes = System.Text.Encoding.UTF8.GetBytes(userPass);
            var userPassBase64 = System.Convert.ToBase64String(userPassBytes);
            return userPassBase64;
        }
    }

    public class BasicAuthenticatedHttpClient
    {
        public static HttpClient GetClient(BasicAuthenticatedHttpClientOptions options)
        {
            var msgHandler = new BasicAuthenticatedHttpMessageHandler(options);
            return new HttpClient(msgHandler);
        }
    }
}