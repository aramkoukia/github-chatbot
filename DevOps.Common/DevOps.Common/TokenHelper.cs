using DevOps.Interfaces;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Common
{
    public class TokenHelper : ITokenHelper
    {
        public async Task<string> GetGithubToken(Activity activity)
        {
            StateClient stateClient = activity.GetStateClient();
            BotState botState = new BotState(stateClient);
            BotData botData = await botState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            return botData.GetProperty<string>("AccessToken");
        }

    }
}
