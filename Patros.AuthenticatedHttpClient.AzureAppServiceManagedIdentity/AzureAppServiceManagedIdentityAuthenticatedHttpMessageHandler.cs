﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Services.AppAuthentication;
using System.Diagnostics.CodeAnalysis;

namespace Patros.AuthenticatedHttpClient
{
    public class AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private string _resourceId;
        private AzureServiceTokenProvider _azureServiceTokenProvider = new AzureServiceTokenProvider();

        public AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler(AzureAppServiceManagedIdentityAuthenticatedHttpClientOptions options, HttpMessageHandler innerHandler = null)
        {
            InnerHandler = innerHandler ?? new HttpClientHandler();

            _resourceId = options.ResourceId;
        }

        [ExcludeFromCodeCoverage]
        internal virtual async Task<string> GetAccessTokenAsync()
        {
            return await _azureServiceTokenProvider.GetAccessTokenAsync(_resourceId);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await GetAccessTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                accessToken);
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}