using DevOps.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOps.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DevOps.Github
{
    public class GithubRepository : IGithubRepository
    {
        private static string GithubBaseUrl = "https://api.github.com";
        private static string CodeRepositoriesUrl = "/user/repos";

        #region Repositories

        public async Task<IEnumerable<CodeRepository>> GetCodeRepositories(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Anything");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
            HttpResponseMessage response = await httpClient.GetAsync(GithubBaseUrl + CodeRepositoriesUrl);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<GithubRepositoryDto>>(responseString);
            return DataMapper.Map(result);
        }

        #endregion

        #region Issues

        public async Task<Issue> CreateIssue(Issue issue, string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Anything");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);

            var param = JsonConvert.SerializeObject(new { title = issue.Title, body = issue.Body });
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");

            // TODO: the repo and owner should not be hardcoded, user should select them
            var issuesUrl = $"/repos/daveos/{issue.Repository}/issues";

            HttpResponseMessage response = await httpClient.PostAsync(GithubBaseUrl + issuesUrl, contentPost);
            return issue;
        }

        public async Task<Issue> GetIssue(string issueId, string repository, string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Anything");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);

            // TODO: the repo and owner should not be hardcoded, user should select them
            var githubIssueUrl = $"/repos/daveos/{repository}/issues/{issueId}";
            HttpResponseMessage response = await httpClient.GetAsync(GithubBaseUrl + githubIssueUrl);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GithubIssueDto>(responseString);
            return DataMapper.Map(result);
        }

        #endregion

        #region Pull Requests

        public async Task<PullRequest> CreatePullRequest(PullRequest pullRequest, string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Anything");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);

            var param = JsonConvert.SerializeObject(new { title = pullRequest.Title, body = pullRequest.Body , head = pullRequest.Head, @base = pullRequest.Base, maintainer_can_modify = pullRequest.MaintainerCanModify });
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");

            // TODO: the repo and owner should not be hardcoded, user should select them
            var issuesUrl = $"/repos/daveos/{pullRequest.Repository}/pulls";

            HttpResponseMessage response = await httpClient.PostAsync(GithubBaseUrl + issuesUrl, contentPost);
            return pullRequest;
        }

        #endregion

    }
}
