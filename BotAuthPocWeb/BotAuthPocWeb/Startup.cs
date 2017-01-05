using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BotAuthPocWeb.Startup))]
namespace BotAuthPocWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
