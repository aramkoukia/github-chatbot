using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace DevOps.IntegrationTests
{
    public static class UnityHelper
    {
        private static IUnityContainer _unityContainer;

        static UnityHelper()
        {
            _unityContainer = new UnityContainer();
        }

        public static IUnityContainer UnityContainer
        {
            get { return _unityContainer; }
            set { _unityContainer = value; }
        }

        static public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        static public IEnumerable<T> ResolveAll<T>()
        {
            return _unityContainer.ResolveAll<T>();
        }
    }
}
