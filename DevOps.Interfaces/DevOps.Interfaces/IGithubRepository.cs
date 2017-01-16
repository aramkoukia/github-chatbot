using DevOps.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevOps.Interfaces
{
    public interface IGithubRepository
    {
        #region Repositories

        Task<IEnumerable<CodeRepository>> GetCodeRepositories(string token);

        #endregion

        #region Issues

        Task<Issue> CreateIssue(Issue issue, string token);

        Task<Issue> GetIssue(string issueId, string repository, string token);

        #endregion

        #region Pull Requests

        Task<PullRequest> CreatePullRequest(PullRequest pullRequest, string token);

        #endregion


    }
}
