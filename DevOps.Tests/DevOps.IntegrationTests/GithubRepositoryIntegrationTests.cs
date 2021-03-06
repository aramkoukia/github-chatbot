﻿using System;
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
            var token = "oathtoken";
            var result = await _githubRepository.GetCodeRepositories(token);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GithubRepository_CreateIssue_HappyPath()
        {
            var token = "oathtoken";
            var result = await _githubRepository.CreateIssue(new Issue() { Body = "test body" , Title = "Test title"}, token);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GithubRepository_CreatePullRequest_HappyPath()
        {
            var token = "oathtoken";
            var result = await _githubRepository.CreatePullRequest(new PullRequest() {  Base = "master", Title = "test PR 1 from Bot", Body = "PR Body", Head = "featurebranch1", MaintainerCanModify = true,  Repository = "DevOrb" }, token);
            Assert.IsNotNull(result);
        }

    }
}
