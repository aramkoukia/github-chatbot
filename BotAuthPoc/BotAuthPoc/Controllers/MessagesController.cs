﻿using System;
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
                if (activity.Text == "login")
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                    Activity replyToConversation = activity.CreateReply();
                    replyToConversation.Recipient = activity.From;
                    replyToConversation.Type = "message";

                    replyToConversation.Attachments = new List<Attachment>();
                    List<CardAction> cardButtons = new List<CardAction>();
                    CardAction plButton = new CardAction()
                    {
                        Value = $"{System.Configuration.ConfigurationManager.AppSettings["AppWebSite"]}/Home/Login?userid={HttpUtility.UrlEncode(activity.From.Id)}",
                        Type = "signin",
                        Title = "VSTS"
                    };

                    CardAction plButton1 = new CardAction()
                    {
                        Value = $"{System.Configuration.ConfigurationManager.AppSettings["AppWebSite"]}/Home/Login?userid={HttpUtility.UrlEncode(activity.From.Id)}",
                        Type = "signin",
                        Title = "GitHub"
                    };


                    cardButtons.Add(plButton);
                    cardButtons.Add(plButton1);
                    SigninCard plCard = new SigninCard("Please login", new List<CardAction>() { plButton, plButton1 });
                    Attachment plAttachment = plCard.ToAttachment();
                    replyToConversation.Attachments.Add(plAttachment);

                    var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
                }
                else if (activity.Text == "get mail")
                {
                    // Get access token from bot state
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    StateClient stateClient = activity.GetStateClient();
                    BotState botState = new BotState(stateClient);
                    BotData botData = await botState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                    string token = botData.GetProperty<string>("AccessToken");

                    // Get recent 10 e-mail from Office 365
                    HttpClient cl = new HttpClient();
                    var acceptHeader =
                        new MediaTypeWithQualityHeaderValue("application/json");
                    cl.DefaultRequestHeaders.Accept.Add(acceptHeader);
                    cl.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage httpRes =
                      await cl.GetAsync("https://outlook.office365.com/api/v1.0/me/messages?$orderby=DateTimeSent%20desc&$top=10&$select=Subject,From");
                    if (httpRes.IsSuccessStatusCode)
                    {
                        var strRes = httpRes.Content.ReadAsStringAsync().Result;
                        JObject jRes = await httpRes.Content.ReadAsAsync<JObject>();
                        JArray jValue = (JArray)jRes["value"];
                        foreach (JObject jItem in jValue)
                        {
                            Activity reply = activity.CreateReply($"Subject={((JValue)jItem["Subject"]).Value}");
                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                    else
                    {
                        Activity reply = activity.CreateReply("Failed to get e-mail.\n\nPlease type \"login\" before you get e-mail.");
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }
                }
                else
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    Activity reply = activity.CreateReply("# Bot Help\n\nlogin -- Login to Office 365\n\nget mail -- Get your e-mail from Office 365");
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