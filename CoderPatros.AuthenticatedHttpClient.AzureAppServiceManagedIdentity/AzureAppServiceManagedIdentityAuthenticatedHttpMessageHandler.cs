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
            _resourceId = options.ResourceId;
        }

        public AzureAppServiceManagedIdentityAuthenticatedHttpMessageHandler(
            AzureAppServiceManagedIdentityAuthenticatedHttpClientOptions options, 
            HttpMessageHandler innerHandler) : this(options)
        {
            InnerHandler = innerHandler;
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