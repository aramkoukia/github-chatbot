using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using DevOps.Interfaces;
using DevOps.Common;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs;
using DevOps.Dialogs;

namespace DevOps.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly ICodeRepositoryDialogs _codeRepositoryWorkflows;
        private readonly ITokenHelper _tokenHelper;

        public MessagesController(ICodeRepositoryDialogs codeRepositoryWorkflows, ITokenHelper tokenHelper)
        {
            if (codeRepositoryWorkflows == null)
                throw new ArgumentNullException(nameof(codeRepositoryWorkflows));

            _codeRepositoryWorkflows = codeRepositoryWorkflows;
            _tokenHelper = tokenHelper;
        }
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity!= null && activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new RootDialog());

                //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //var replyToConversation = activity.CreateReply();
                //switch (activity.Text.ToLower())
                //{
                //    case BotCommands.Login:

                //        var token = await _tokenHelper.GetGithubToken(activity);
                //        if (string.IsNullOrEmpty(token))
                //        {
                //            replyToConversation.Recipient = activity.From;
                //            replyToConversation.Type = "message";

                //            replyToConversation.Attachments = new List<Attachment>();
                //            List<CardAction> cardButtons = new List<CardAction>();
                //            CardAction plButton = new CardAction()
                //            {
                //                Value = "https://orbitaldevopsweb.azurewebsites.net/Home/Github?username=" + Uri.EscapeDataString(activity.From.Id),
                //                Type = "signin",
                //                Title = "GitHub"
                //            };

                //            cardButtons.Add(plButton);
                //            SigninCard plCard = new SigninCard("Please login", new List<CardAction>() { plButton });
                //            Attachment plAttachment = plCard.ToAttachment();
                //            replyToConversation.Attachments.Add(plAttachment);

                //            await connector.Conversations.SendToConversationAsync(replyToConversation);
                //        }
                //        else
                //        {
                //            var reply1 = activity.CreateReply("You are already signed in.");
                //            await connector.Conversations.ReplyToActivityAsync(reply1);
                //        }

                //        break;

                //    case BotCommands.ListRepositories:
                //        var repositories = await _developementWorkflows.GetCodeRepositories(activity);
                //        if(repositories == null || !repositories.Any())
                //        { 
                //            var reply2 = activity.CreateReply("You are already signed in.");
                //            await connector.Conversations.ReplyToActivityAsync(reply2);
                //        }

                //        replyToConversation = activity.CreateReply();
                //        replyToConversation.Recipient = activity.From;
                //        replyToConversation.Type = "message";
                //        replyToConversation.Text = $"You have access to {repositories.Count()} Code Repositories. See following for details:";
                //        await connector.Conversations.SendToConversationAsync(replyToConversation);

                //        foreach (var item in repositories)
                //        {
                //            replyToConversation = activity.CreateReply();
                //            replyToConversation.Recipient = activity.From;
                //            replyToConversation.Type = "message";
                //            replyToConversation.Text = $"Fullname: {item.FullName}, Name: {item.Name}, Id: {item.Id}, Owner: {item.Owner}.";
                //            await connector.Conversations.SendToConversationAsync(replyToConversation);
                //        }
                //        break;

                //    default:
                //        var reply3 = activity.CreateReply("Not supported.");
                //        await connector.Conversations.ReplyToActivityAsync(reply3);
                //        break;
                //}


            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }


        //private Activity HandleSystemMessage(Activity message)
        //{
        //    if (message.Type == ActivityTypes.DeleteUserData)
        //    {
        //        // Implement user deletion here
        //        // If we handle user deletion, return a real message
        //    }
        //    else if (message.Type == ActivityTypes.ConversationUpdate)
        //    {
        //        // Handle conversation state changes, like members being added and removed
        //        // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
        //        // Not available in all channels
        //    }
        //    else if (message.Type == ActivityTypes.ContactRelationUpdate)
        //    {
        //        // Handle add/remove from contact lists
        //        // Activity.From + Activity.Action represent what happened
        //    }
        //    else if (message.Type == ActivityTypes.Typing)
        //    {
        //        // Handle knowing tha the user is typing
        //    }
        //    else if (message.Type == ActivityTypes.Ping)
        //    {
        //    }

        //    return null;
        //}
    }
}