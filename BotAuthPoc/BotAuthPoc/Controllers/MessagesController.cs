using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace BotAuthPoc
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {

            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                if (activity.Text == "name")
                {
                    Activity reply = activity.CreateReply($"{Uri.EscapeDataString(activity.From.Id)}");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                if (activity.Text == "token")
                {
                    StateClient stateClient = activity.GetStateClient();
                    BotState botState = new BotState(stateClient);
                    BotData botData = await botState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                    string token = botData.GetProperty<string>("AccessToken");

                    Activity reply = activity.CreateReply($"token: {token}");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text == "login")
                {
                    Activity replyToConversation = activity.CreateReply();
                    replyToConversation.Recipient = activity.From;
                    replyToConversation.Type = "message";

                    replyToConversation.Attachments = new List<Attachment>();
                    List<CardAction> cardButtons = new List<CardAction>();
                    //CardAction plButton = new CardAction()
                    //{
                    //    Value = "http://botauthpocweb.azurewebsites.net/Home/Github",
                    //    Type = "signin",
                    //    Title = "VSTS"
                    //};

                    CardAction plButton = new CardAction()
                    {
                        Value = "https://botauthpocweb.azurewebsites.net/Home/Github?username=" + Uri.EscapeDataString(activity.From.Id),
                        Type = "signin",
                        Title = "GitHub"
                    };

                    cardButtons.Add(plButton);
                    //cardButtons.Add(plButton1);
                    SigninCard plCard = new SigninCard("Please login", new List<CardAction>() { plButton });
                    Attachment plAttachment = plCard.ToAttachment();
                    replyToConversation.Attachments.Add(plAttachment);

                    var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
                }
                else if (activity.Text == "repos")
                {
                    // Get access token from bot state
                    try
                    {
                        StateClient stateClient = activity.GetStateClient();
                        BotState botState = new BotState(stateClient);
                        BotData botData = await botState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                        string token = botData.GetProperty<string>("AccessToken");

                        // Get recent 10 e-mail from Office 365
                        HttpClient cl = new HttpClient();
                        //var acceptHeader = new MediaTypeWithQualityHeaderValue("application/json");
                        //cl.DefaultRequestHeaders.Accept.Add(acceptHeader);

                        //Activity reply = activity.CreateReply($"token: {token}");
                        //await connector.Conversations.ReplyToActivityAsync(reply);

                        //var reply = activity.CreateReply($"token {token}");
                        //await connector.Conversations.ReplyToActivityAsync(reply);

                        cl.DefaultRequestHeaders.Add("User-Agent", "Anything");

                        cl.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
                        HttpResponseMessage httpRes = await cl.GetAsync("https://api.github.com/user/repos");

                        //reply = activity.CreateReply($"url https://api.github.com/user/repos");
                        //await connector.Conversations.ReplyToActivityAsync(reply);

                        if (httpRes.IsSuccessStatusCode)
                        {

                            //var reply = activity.CreateReply($"was successful {httpRes.IsSuccessStatusCode}");
                            //await connector.Conversations.ReplyToActivityAsync(reply);

                            var strRes = await httpRes.Content.ReadAsStringAsync();
                            var reply = activity.CreateReply(strRes.Substring(1,50));
                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                        else
                        {
                            //var reply = activity.CreateReply($"was not successful {httpRes.IsSuccessStatusCode}");
                            //await connector.Conversations.ReplyToActivityAsync(reply);

                            var reply = activity.CreateReply("Failed to get repos.\n\nPlease type \"login\" before you get repos.");
                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                    catch (Exception ex)
                    {
                        Activity reply = activity.CreateReply($"error: {ex.InnerException.Message}");
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }

                }
                else
                {
                    Activity reply = activity.CreateReply("# Bot Help\n\nlogin -- Login to github");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
            }
            else
            {
                HandleSystemMessage(activity);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}