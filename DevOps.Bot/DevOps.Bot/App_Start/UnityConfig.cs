using DevOps.Github;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace DevOps.Bot
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            container.AddGithubIntegrationTypes();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            return container;
        }
    }
}