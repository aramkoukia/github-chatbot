using DevOps.Interfaces;
using Microsoft.Practices.Unity;
using System;

namespace DevOps.Common
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer AddCommonTypes(this IUnityContainer unityContainer)
        {
            if (unityContainer == null)
                throw new ArgumentNullException("unityContainer");

            unityContainer.RegisterType<ITokenHelper, TokenHelper>();
            return unityContainer;
        }
    }
}
