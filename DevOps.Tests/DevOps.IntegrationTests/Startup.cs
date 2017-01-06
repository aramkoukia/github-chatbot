using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOps.IntegrationTests;
using DevOps.Bot;
using DevOps.Github;

namespace DevOps.IntegrationTests
{
    [TestClass]
    public class Startup
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            UnityHelper.UnityContainer = UnityConfig.RegisterComponents();
            UnityHelper.UnityContainer.AddGithubIntegrationTypes();
        }

    }
}
