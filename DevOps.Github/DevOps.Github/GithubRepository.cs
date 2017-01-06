using DevOps.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOps.Contracts;

namespace DevOps.Github
{
    public class GithubRepository : IGithubRepository
    {
        public Task<IEnumerable<CodeRepository>> GetCodeRepositories()
        {
            throw new NotImplementedException();
        }
    }
}
