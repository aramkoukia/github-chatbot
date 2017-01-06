using DevOps.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Contracts;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace DevOps.Dialogs
{
    public class CodeRepositoryDialogs : ICodeRepositoryDialogs, IDialog<object>
    {
        private readonly IGithubRepository _githubRepository;
        private readonly ITokenHelper _tokenHelper;

        public CodeRepositoryDialogs()
        {

        }

        public CodeRepositoryDialogs(IGithubRepository githubRepository, ITokenHelper tokenHelper)
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

        public Task StartAsync(IDialogContext context)
        {
            throw new NotImplementedException();
        }
    }
}
