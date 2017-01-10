//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Bot.Builder.Dialogs;
//using Microsoft.Bot.Connector;
//using DevOps.Interfaces;

//namespace DevOps.Dialogs
//{
//    [Serializable]
//    public class RootDialog : IDialog<object>
//    {
//        //private readonly ICodeRepositoryDialogs _codeRepositoryWorkflows;
//        public RootDialog()
//        {
//            //if (codeRepositoryWorkflows == null)
//            //    throw new ArgumentNullException(nameof(codeRepositoryWorkflows));

//            //_codeRepositoryWorkflows = codeRepositoryWorkflows;

//        }

//        private const string RepositoriesOption = "repos";

//        private const string IssuesOption = "issues";

//        public async Task StartAsync(IDialogContext context)
//        {
//            context.Wait(this.MessageReceivedAsync);
//        }

//        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
//        {
//            var message = await result;

//            if (message.Text.ToLower().Contains("help") || message.Text.ToLower().Contains("support") || message.Text.ToLower().Contains("problem"))
//            {
//                await context.Forward(new SupportDialog(), ResumeAfterSupportDialog, message, CancellationToken.None);
//            }
//            else
//            {
//                this.ShowOptions(context);
//            }
//        }

//        private void ShowOptions(IDialogContext context)
//        {
//            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { IssuesOption, RepositoriesOption }, "Do you want to work with Issues or Repositories?", "Not a valid option", 3);
//        }

//        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
//        {
//            try
//            {
//                string optionSelected = await result;

//                switch (optionSelected)
//                {
//                    case RepositoriesOption:
//                        context.Call(new CodeRepositoryDialogs(), ResumeAfterOptionDialog);
//                        break;

//                    case IssuesOption:
//                        context.Call(new IssuesDialog(), ResumeAfterOptionDialog);
//                        break;
//                }
//            }
//            catch (TooManyAttemptsException ex)
//            {
//                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

//                context.Wait(MessageReceivedAsync);
//            }
//        }

//        private async Task ResumeAfterSupportDialog(IDialogContext context, IAwaitable<int> result)
//        {
//            var ticketNumber = await result;

//            await context.PostAsync($"Thanks for contacting our support team. Your ticket number is {ticketNumber}.");
//            context.Wait(MessageReceivedAsync);
//        }

//        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
//        {
//            try
//            {
//                var message = await result;
//            }
//            catch (Exception ex)
//            {
//                await context.PostAsync($"Failed with message: {ex.Message}");
//            }
//            finally
//            {
//                context.Wait(this.MessageReceivedAsync);
//            }
//        }
//    }
//}
