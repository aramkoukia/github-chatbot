using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevOps.Interfaces;
using System.Threading.Tasks;
using DevOps.Contracts;

namespace DevOps.IntegrationTests
{
    [TestClass]
    public class GithubRepositoryIntegrationTests
    {
        private IGithubRepository _githubRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _githubRepository = UnityHelper.Resolve<IGithubRepository>();
        }

        [TestMethod]
        public async Task GithubRepository_GetCodeRepositories_HappyPath()
        {
            var token = "961ac7b8c3c654fde4d437d3502a3b8aece8eb22";
            var result = await _githubRepository.GetCodeRepositories(token);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GithubRepository_CreateIssue_HappyPath()
        {
            var token = "961ac7b8c3c654fde4d437d3502a3b8aece8eb22";
            var result = await _githubRepository.CreateIssue(new Issue() { Body = "test body" , Title = "Test title"}, token);
            Assert.IsNotNull(result);
        }

    }
}
