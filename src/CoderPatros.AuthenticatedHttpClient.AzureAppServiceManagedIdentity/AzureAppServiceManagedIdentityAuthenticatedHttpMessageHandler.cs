using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Services.AppAuthentication;
using System.Diagnostics.CodeAnalysis;

namespace CoderPatros.AuthenticatedHttpClient
{
    public class AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private readonly string _resourceId;
        private readonly AzureServiceTokenProvider _azureServiceTokenProvider = new AzureServiceTokenProvider();

        public AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler(
            AzureAppServiceManagedIdentityAuthenticatedHttpClientOptions options)
        {
            Contract.Requires(options != null);

            _resourceId = options.ResourceId;
        }

        public AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler(
            AzureAppServiceManagedIdentityAuthenticatedHttpClientOptions options, 
            HttpMessageHandler innerHandler) : this(options)
        {
            Contract.Requires(options != null);

            InnerHandler = innerHandler;
        }

        [ExcludeFromCodeCoverage]
        internal virtual async Task<string> GetAccessTokenAsync()
        {
            return await _azureServiceTokenProvider.GetAccessTokenAsync(_resourceId).ConfigureAwait(false);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Contract.Requires(request != null);

            var accessToken = await GetAccessTokenAsync().ConfigureAwait(false);

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                accessToken);
            
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}