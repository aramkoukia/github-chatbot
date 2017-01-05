using DevOps.Github.Auth;
using Microsoft.AspNet.Identity;
using Owin;
using System.Configuration;

namespace DevOpsBot.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login")
            //});

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseGitHubAuthentication(ConfigurationManager.AppSettings["GithubClientId"], ConfigurationManager.AppSettings["GithubClientSecret"]);
        }
    }
}