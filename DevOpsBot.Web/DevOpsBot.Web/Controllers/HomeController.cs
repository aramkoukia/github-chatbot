using Microsoft.Bot.Connector;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DevOpsBot.Web.Controllers
{
    public class HomeController : Controller
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GitHub(string username)
        {
            Session["UserIdentifier"] = username;
            return new ChallengeResult("GitHub", Url.Action("ExternalLoginCallback", "Home", new { ReturnUrl = "/Home/Index" }), username);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loggedInUserInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loggedInUserInfo == null)
                return RedirectToAction("GitHub");

            var botCred = new MicrosoftAppCredentials(ConfigurationManager.AppSettings["MicrosoftAppId"], ConfigurationManager.AppSettings["MicrosoftAppPassword"]);

            var stateClient = new StateClient(botCred);
            var botState = new BotState(stateClient);
            var botData = new BotData(eTag: "*");
            var accessToken = loggedInUserInfo.ExternalIdentity.Claims.FirstOrDefault(m => m.Type == "urn:github:accesstoken").Value;
            botData.SetProperty("AccessToken", accessToken);
            await stateClient.BotState.SetUserDataAsync("skype", Session["UserIdentifier"].ToString(), botData);

            return View("Index", loggedInUserInfo);
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
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties()
                {
                    RedirectUri = RedirectUri
                };

                if (UserId != null)
                    properties.Dictionary[XsrfKey] = UserId;
 
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}