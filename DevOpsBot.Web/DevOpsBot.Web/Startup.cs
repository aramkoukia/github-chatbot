using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevOpsBot.Web.Startup))]
namespace DevOpsBot.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
