using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Patros.AuthenticatedHttpClient
{
    // implementation shamelessly ripped off from the Azure sample
    // https://github.com/Azure-Samples/active-directory-dotnet-daemon/blob/master/TodoListDaemon/Program.cs
    public class AzureAdAuthenticatedHttpClientOptions {
        /// <summary>
        /// The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        /// </summary>
        /// <value></value>
        public string AadInstance { get; set; } = "https://login.microsoftonline.com/{0}";

        /// <summary>
        /// The Tenant is the name of the Azure AD tenant in which this application is registered.
        /// </summary>
        /// <value></value>
        public string Tenant { get; set; }

        /// <summary>
        /// The Client ID is used by the application to uniquely identify itself to Azure AD.
        /// </summary>
        /// <value></value>
        public string ClientId { get; set; }

        /// <summary>
        /// The App Key is a credential used by the application to authenticate to Azure AD.
        /// </summary>
        /// <value></value>
        public string AppKey { get; set; }

        /// <summary>
        /// The Resource Id is the id of the service you are contacting/authenticating to.
        /// </summary>
        /// <value></value>
        public string ResourceId { get; set; }
    }

    public class AzureAdAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private string _resourceId;
        private AuthenticationContext _authContext;
        private ClientCredential _clientCredential;

        public AzureAdAuthenticatedHttpMessageHandler(AzureAdAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            InnerHandler = innerHandler ?? new HttpClientHandler();
            
            _resourceId = options.ResourceId;

            var authority = String.Format(CultureInfo.InvariantCulture, options.AadInstance, options.Tenant);
            _authContext = new AuthenticationContext(authority);

            _clientCredential = new ClientCredential(options.ClientId, options.AppKey);
        }

        internal virtual async Task<string> AcquireAccessTokenAsync()
        {
            var result = await _authContext.AcquireTokenAsync(_resourceId, _clientCredential);
            return result?.AccessToken;
        }

        private async Task<string> AcquireTokenWithRetriesAsync(CancellationToken cancellationToken)
        {
            //
            // Get an access token from Azure AD using client credentials.
            // If the attempt to get a token fails because the server is unavailable, retry twice after 3 seconds each.
            //
            string token = null;
            int retryCount = 0;
            bool retry = false;

            do
            {
                retry = false;
                try
                {
                    // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
                    token = await AcquireAccessTokenAsync();
                }
                catch (AdalException ex)
                {
                    if (ex.ErrorCode == "temporarily_unavailable" && !cancellationToken.IsCancellationRequested)
                    {
                        retry = true;
                        retryCount++;
                        Thread.Sleep(3000);
                    }

                    // Console.WriteLine(
                    //     String.Format("An error occurred while acquiring a token\nTime: {0}\nError: {1}\nRetry: {2}\n",
                    //     DateTime.Now.ToString(),
                    //     ex.ToString(),
                    //     retry.ToString()));
                }

            } while ((retry == true) && (retryCount < 3) && !cancellationToken.IsCancellationRequested);

            return token;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await AcquireTokenWithRetriesAsync(cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                accessToken);
            
            return await base.SendAsync(request, cancellationToken);
        }
    }

    public static class AzureAdAuthenticatedHttpClient
    {
        public static HttpClient GetClient(AzureAdAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            var msgHandler = new AzureAdAuthenticatedHttpMessageHandler(options, innerHandler);
            return new HttpClient(msgHandler);
        }
    }
}