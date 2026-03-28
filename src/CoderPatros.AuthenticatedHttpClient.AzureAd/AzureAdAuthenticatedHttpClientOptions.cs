namespace CoderPatros.AuthenticatedHttpClient
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
}