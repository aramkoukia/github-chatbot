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
        // TODO: the repo and owner should not be hardcoded, user should select them
        private static string IssuesUrl = "/repos/daveos/DevOrb/issues";

        public async Task<IEnumerable<CodeRepository>> GetCodeRepositories(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Anything");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
            HttpResponseMessage response = await httpClient.GetAsync(GithubBaseUrl + CodeRepositoriesUrl);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<GithubRepositoryDto>>(responseString);
            return Map(result);
        }

        private IEnumerable<CodeRepository> Map(List<GithubRepositoryDto> result)
        {
            if (result == null)
                return null;

            return result.Select(a => new CodeRepository
            {
                FullName = a.full_name,
                Id = a.id.ToString(),
                Name = a.name,
                Owner = a.owner.login,
                Url = a.url
            }).ToList();
        }

        public async Task<Issue> CreateIssue(Issue issue, string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Anything");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);

            var param = JsonConvert.SerializeObject(new { title = issue.Title, body = issue.Body });
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(GithubBaseUrl + IssuesUrl, contentPost);
            return issue;
        }

    }
}
