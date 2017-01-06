using DevOps.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOps.Contracts;
using Microsoft.Bot.Connector;

namespace DevOps.Workflows
{
    public class DevelopmentWorkflows : IDevelopementWorkflows
    {
        private readonly IGithubRepository _githubRepository;
        private readonly ITokenHelper _tokenHelper;

        public DevelopmentWorkflows(IGithubRepository githubRepository, ITokenHelper tokenHelper)
        {
            if (githubRepository == null)
                throw new ArgumentNullException(nameof(githubRepository));

            if (tokenHelper == null)
                throw new ArgumentNullException(nameof(_tokenHelper));

            _githubRepository = githubRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<IEnumerable<CodeRepository>> GetCodeRepositories(Activity activity)
        {
            var token = await _tokenHelper.GetGithubToken(activity);
            return await _githubRepository.GetCodeRepositories(token);
        }
    }
}
