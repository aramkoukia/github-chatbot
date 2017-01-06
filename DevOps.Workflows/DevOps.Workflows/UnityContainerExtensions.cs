using DevOps.Interfaces;
using Microsoft.Practices.Unity;
using System;

namespace DevOps.Workflows
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer AddWorkflowTypes(this IUnityContainer unityContainer)
        {
            if (unityContainer == null)
                throw new ArgumentNullException("unityContainer");

            unityContainer.RegisterType<IDevelopementWorkflows, DevelopmentWorkflows>();
            return unityContainer;
        }
    }
}
