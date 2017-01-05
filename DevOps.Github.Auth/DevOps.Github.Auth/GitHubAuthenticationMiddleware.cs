using System;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace DevOps.Github.Auth
{
    public class GitHubAuthenticationMiddleware : AuthenticationMiddleware<GitHubAuthenticationOptions>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public GitHubAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, GitHubAuthenticationOptions options) : base(next, options)
        {
            if (string.IsNullOrWhiteSpace(Options.ClientId))
                throw new ArgumentException(nameof(options.ClientId));

            if (string.IsNullOrWhiteSpace(Options.ClientSecret))
                throw new ArgumentException(nameof(options.ClientSecret));

            _logger = app.CreateLogger<GitHubAuthenticationMiddleware>();

            if (Options.Provider == null)
                Options.Provider = new GitHubAuthenticationProvider();

            if (Options.StateDataFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof (GitHubAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v1");
                Options.StateDataFormat = new PropertiesDataFormat(dataProtector);
            }

            if (string.IsNullOrEmpty(Options.SignInAsAuthenticationType))
                Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();

            _httpClient = new HttpClient(ResolveHttpMessageHandler(Options))
            {
                Timeout = Options.BackchannelTimeout,
                MaxResponseContentBufferSize = 1024*1024*10,
            };
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft Owin GitHub middleware");
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
        }

        /// <summary>
        ///     Provides the <see cref="T:Microsoft.Owin.Security.Infrastructure.AuthenticationHandler" /> object for processing
        ///     authentication-related requests.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:Microsoft.Owin.Security.Infrastructure.AuthenticationHandler" /> configured with the
        ///     <see cref="T:Owin.Security.Providers.GitHub.GitHubAuthenticationOptions" /> supplied to the constructor.
        /// </returns>
        protected override AuthenticationHandler<GitHubAuthenticationOptions> CreateHandler()
        {
            return new GitHubAuthenticationHandler(_httpClient, _logger);
        }

        private static HttpMessageHandler ResolveHttpMessageHandler(GitHubAuthenticationOptions options)
        {
            var handler = options.BackchannelHttpHandler ?? new WebRequestHandler();

            // If they provided a validator, apply it or fail.
            if (options.BackchannelCertificateValidator == null) return handler;
            // Set the cert validate callback
            var webRequestHandler = handler as WebRequestHandler;
            if (webRequestHandler == null)
            {
                throw new InvalidOperationException("An ICertificateValidator cannot be specified at the same time as an HttpMessageHandler unless it is a WebRequestHandler.");
            }
            webRequestHandler.ServerCertificateValidationCallback = options.BackchannelCertificateValidator.Validate;

            return handler;
        }
    }
}