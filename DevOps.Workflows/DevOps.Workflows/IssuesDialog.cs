using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using DevOps.Contracts;
using DevOps.Dialogs;
using System.Web;
using DevOps.Interfaces;
using DevOps.Github;

namespace DevOps.Dialogs
{
    [Serializable]
    public class IssuesDialog : IDialog<object>
    {
        private readonly IGithubRepository _githubRepository;
        private readonly ITokenHelper _tokenHelper;

        public IssuesDialog()
        {

        }

        public IssuesDialog(IGithubRepository githubRepository, ITokenHelper tokenHelper)
        {
            _githubRepository = githubRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to issues manager.");

            var issuesFormDialog = FormDialog.FromForm(BuildIssuesForm, FormOptions.PromptInStart);

            context.Call(issuesFormDialog, ResumeAfterIssuesFormDialog);
        }

        private IForm<IssuesQuery> BuildIssuesForm()
        {
            OnCompletionAsyncDelegate<IssuesQuery> processIssuesCreation = async (context, state) =>
            {
                // TODO: cast IActivity to Activity
                var token = "961ac7b8c3c654fde4d437d3502a3b8aece8eb22"; //_tokenHelper.GetGithubToken(context.Activity);
                // TODO: use unity and the interface instead
                var repo = new GithubRepository();
                var result = await repo.CreateIssue(new Issue() { Title = state.Title, Body = state.Body }, token);
                await context.PostAsync($"Ok. Issue created with title: {state.Title}.");

                //await context.PostAsync($"Ok. Creating an issue with title: {state.Title}.");
            };

            return new FormBuilder<IssuesQuery>()
                //.Field(nameof(IssuesQuery.Title))
                //.Message("Creating an issue with title: {Title}...")
                .AddRemainingFields()
                .OnCompletion(processIssuesCreation)
                .Build();
        }

        private async Task ResumeAfterIssuesFormDialog(IDialogContext context, IAwaitable<IssuesQuery> result)
        {
            try
            {
                var searchQuery = await result;

                var issues = await GetIssuesAsync(searchQuery);

                await context.PostAsync($"I found in total {issues.Count()} issues assigned to you:");

                var resultMessage = context.MakeMessage();
                resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                resultMessage.Attachments = new List<Attachment>();

                foreach (var issue in issues)
                {
                    HeroCard heroCard = new HeroCard()
                    {
                        Title = "Title",
                        Subtitle = "Issue Body",
                        Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "More details",
                                Type = ActionTypes.OpenUrl,
                                Value = $"https://koukia.ca"
                            }
                        }
                    };

                    resultMessage.Attachments.Add(heroCard.ToAttachment());
                }

                await context.PostAsync(resultMessage);
            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation. Quitting from the IssuesDialog";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }
            finally
            {
                context.Done<object>(null);
            }
        }

        private async Task<IEnumerable<Issue>> GetIssuesAsync(IssuesQuery searchQuery)
        {
            var issues = new List<Issue>();
            for (int i = 1; i <= 5; i++)
            {
                var random = new Random(i);
                Issue issue = new Issue()
                {
                };

                issues.Add(issue);
            }

            return issues;
        }
    }
}