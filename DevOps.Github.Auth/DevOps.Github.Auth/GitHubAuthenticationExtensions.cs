﻿using System;
using Owin;

namespace DevOps.Github.Auth
{
    public static class GitHubAuthenticationExtensions
    {
        public static IAppBuilder UseGitHubAuthentication(this IAppBuilder app,
            GitHubAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            app.Use(typeof(GitHubAuthenticationMiddleware), app, options);

            return app;
        }

        public static IAppBuilder UseGitHubAuthentication(this IAppBuilder app, string clientId, string clientSecret)
        {
            return app.UseGitHubAuthentication(new GitHubAuthenticationOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            });
        }
    }
}