using DevOps.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevOps.Interfaces
{
    public interface IGithubRepository
    {
        Task<IEnumerable<CodeRepository>> GetCodeRepositories(string token);

        Task<Issue> CreateIssue(Issue issue, string token);

        Task<Issue> GetIssue(string issueId, string repository, string token);
    }
}
