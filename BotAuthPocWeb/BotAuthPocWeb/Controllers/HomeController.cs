using BotAuthPocWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Bot.Connector;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BotAuthPocWeb.Controllers
{
    public class HomeController : Controller
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        // Used for Shopify external login to provide shopname while building endpoints.
        private const string ShopNameKey = "ShopName";


        public ActionResult GitHub(string username)
        {
            Session["skypeuserid"] = username;
            return new ChallengeResult("GitHub", Url.Action("ExternalLoginCallback", "Home", new { ReturnUrl = "/Home/Index" }), null, null);
        }

        public ActionResult Vsts()
        {
            return new ChallengeResult("Vsts", Url.Action("ExternalLoginCallback", "Home", new { ReturnUrl = "/Home/Index" }), null, null);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            var identity = await AuthenticationManager.GetExternalIdentityAsync("Github");
            if (loginInfo == null)
            {
                return RedirectToAction("GitHub");
            }

            var botCred = new MicrosoftAppCredentials(
                ConfigurationManager.AppSettings["MicrosoftAppId"],
                ConfigurationManager.AppSettings["MicrosoftAppPassword"]);

            var stateClient = new StateClient(botCred);
            BotState botState = new BotState(stateClient);
            BotData botData = new BotData(eTag: "*");
            var accessToken = loginInfo.ExternalIdentity.Claims.FirstOrDefault(m => m.Type == "urn:github:accesstoken").Value;
            botData.SetProperty<string>("AccessToken", accessToken);
            await stateClient.BotState.SetUserDataAsync("skype", Session["skypeuserid"].ToString(), botData);

            // Sign in the user with this external login provider if the user already has a login
            //var user = await UserManager.FindAsync(loginInfo.Login);
            //if (user != null)
            //{
            //await SignInAsync(user, isPersistent: false);
            return View("Index", loginInfo);
            //}
            //else
            //{
                // If the user does not have an account, then prompt the user to create an account
                //ViewBag.ReturnUrl = returnUrl;
                //ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                //return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            //}
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            //var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            //AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId, string shopName)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
                ShopName = shopName;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            public string ShopName { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }

                if (!string.IsNullOrWhiteSpace(ShopName))
                {
                    properties.Dictionary[ShopNameKey] = ShopName;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}