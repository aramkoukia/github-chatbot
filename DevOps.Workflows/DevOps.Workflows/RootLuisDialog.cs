using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using DevOps.Github;
using DevOps.Contracts;

namespace DevOps.Dialogs
{
    [Serializable]
    [LuisModel("9e723e79-c484-477f-9c5f-f5bcbd92d653", "2ecb16643a0d49b28e5188613f192c36")]
    public class RootLuisDialog : LuisDialog<object>
    {
        private const string IssueAssignee = "IssueAssignee";
        private const string IssueTitle = "IssueTitle";
        private const string IssueRepository = "IssueRepository";
        private const string IssueBody = "IssueBody";

        [LuisIntent("Create Issue")]
        public async Task CreateIssue(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            //await context.PostAsync($"Welcome to the Issue manager! analyzing your message: '{message.Text}'...");

            var createIssueQuery = new CreateIssueQuery();

            EntityRecommendation issueTitleEntityRecommendation;
            EntityRecommendation issueRepositoryEntityRecommendation;
            EntityRecommendation issueBodyEntityRecommendation;

            if (result.TryFindEntity(IssueTitle, out issueTitleEntityRecommendation))
            {
                issueTitleEntityRecommendation.Type = "Title";
            }

            if (result.TryFindEntity(IssueRepository, out issueRepositoryEntityRecommendation))
            {
                issueRepositoryEntityRecommendation.Type = "Repository";
            }

            if (result.TryFindEntity(IssueBody, out issueBodyEntityRecommendation))
            {
                issueBodyEntityRecommendation.Type = "Body";
            }


            var createIssueFormDialog = new FormDialog<CreateIssueQuery>(createIssueQuery, BuildCreateIssueForm, FormOptions.PromptInStart, result.Entities);

            context.Call(createIssueFormDialog, ResumeAfterCreateIssueFormDialog);
        }

        [LuisIntent("Search Issue")]
        public async Task SearchIssue(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            //await context.PostAsync($"Welcome to the Issue manager! analyzing your message: '{message.Text}'...");

            var searchIssueQuery = new SearchIssueQuery();

            EntityRecommendation issueTitleEntityRecommendation;
            EntityRecommendation issueRepositoryEntityRecommendation;
            EntityRecommendation issueBodyEntityRecommendation;

            if (result.TryFindEntity(IssueTitle, out issueTitleEntityRecommendation))
            {
                issueTitleEntityRecommendation.Type = "Title";
            }

            if (result.TryFindEntity(IssueRepository, out issueRepositoryEntityRecommendation))
            {
                issueRepositoryEntityRecommendation.Type = "Repository";
            }

            var createIssueFormDialog = new FormDialog<SearchIssueQuery>(searchIssueQuery, BuildCreateIssueForm, FormOptions.PromptInStart, result.Entities);

            context.Call(createIssueFormDialog, ResumeAfterCreateIssueFormDialog);
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hi! Try asking me things like 'create issue with title title of the issue in devops repository', 'create issue in devops repository title server error when user click on sign in'");

            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        private IForm<CreateIssueQuery> BuildCreateIssueForm()
        {
            OnCompletionAsyncDelegate<CreateIssueQuery> processCreateIssue = async (context, state) =>
            {
                var message = "Creating Issue ";
                if (!string.IsNullOrEmpty(state.Title))
                {
                    message += $" with {state.Title}...";
                }
                else if (!string.IsNullOrEmpty(state.Repository))
                {
                    message += $" in {state.Repository} Repository...";
                }

                // TODO: cast IActivity to Activity
                var token = "961ac7b8c3c654fde4d437d3502a3b8aece8eb22"; //_tokenHelper.GetGithubToken(context.Activity);
                // TODO: use unity and the interface instead
                var repo = new GithubRepository();
                var result = await repo.CreateIssue(new Issue() { Title = state.Title, Body = state.Body, Repository = state.Repository }, token);
                await context.PostAsync($"Ok. Issue created with title: {state.Title}.");

                await context.PostAsync(message);


            };

            return new FormBuilder<CreateIssueQuery>()
                .Field(nameof(CreateIssueQuery.Title), (state) => string.IsNullOrEmpty(state.Title))
                .Field(nameof(CreateIssueQuery.Repository), (state) => string.IsNullOrEmpty(state.Repository))
                .Field(nameof(CreateIssueQuery.Body), (state) => string.IsNullOrEmpty(state.Body))
                .OnCompletion(processCreateIssue)
                .Build();
        }

        private IForm<SearchIssueQuery> BuildSearchIssueForm()
        {
            OnCompletionAsyncDelegate<SearchIssueQuery> processSearchIssue = async (context, state) =>
            {
                var message = "Searching Issue ";
                if (!string.IsNullOrEmpty(state.ID))
                {
                    message += $" with {state.ID}...";
                }
                //else if (!string.IsNullOrEmpty(state.Repository))
                //{
                //    message += $" in {state.Repository} Repository...";
                //}

                // TODO: cast IActivity to Activity
                var token = "961ac7b8c3c654fde4d437d3502a3b8aece8eb22"; //_tokenHelper.GetGithubToken(context.Activity);
                // TODO: use unity and the interface instead
                var repo = new GithubRepository();
                var result = await repo.CreateIssue(new Issue() { Title = state.Title, Body = state.Body, Repository = state.Repository }, token);
                //await context.PostAsync($"Ok. Issue created with title: {state.Title}.");

                await context.PostAsync(message);


            };

            return new FormBuilder<CreateIssueQuery>()
                .Field(nameof(CreateIssueQuery.Title), (state) => string.IsNullOrEmpty(state.Title))
                .Field(nameof(CreateIssueQuery.Repository), (state) => string.IsNullOrEmpty(state.Repository))
                .Field(nameof(CreateIssueQuery.Body), (state) => string.IsNullOrEmpty(state.Body))
                .OnCompletion(processSearchIssue)
                .Build();
        }


        private async Task ResumeAfterCreateIssueFormDialog(IDialogContext context, IAwaitable<CreateIssueQuery> result)
        {
            try
            {
                var searchQuery = await result;
                await context.PostAsync($"Issue created.");
                var resultMessage = context.MakeMessage();
                await context.PostAsync(resultMessage);
            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation.";
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

    }
}
