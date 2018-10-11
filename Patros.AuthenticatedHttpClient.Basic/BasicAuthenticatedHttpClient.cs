using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Patros.AuthenticatedHttpClient
{
    public class BasicAuthenticatedHttpClientOptions {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class BasicAuthenticatedHttpClient : HttpClient
    {
        public BasicAuthenticatedHttpClient(BasicAuthenticatedHttpClientOptions options)
        {
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", 
                BasicAuthenticatedHttpClient.GenerateAuthenticationParameter(options.UserId, options.Password));
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
}