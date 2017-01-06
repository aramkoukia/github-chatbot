using DevOps.Interfaces;
using Microsoft.Practices.Unity;
using System;

namespace DevOps.Github
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer AddGithubIntegrationTypes(this IUnityContainer unityContainer)
        {
            if (unityContainer == null)
                throw new ArgumentNullException("unityContainer");

            unityContainer.RegisterType<IGithubRepository, GithubRepository>();
            return unityContainer;
        }
    }
}
