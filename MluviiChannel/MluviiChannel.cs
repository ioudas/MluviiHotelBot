using AdaptiveCards;
using Microsoft.Bot.Builder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicBot.MluviiChannel
{
    public static class MluviiChannel
    {
        public static async Task<Dictionary<string, string>> GetCallParams(ITurnContext context)
        {
            var data = JObject.Parse(@"{ ""Activity"": ""GetCallParams"" }");
            var act = context.Activity.CreateReply();
            act.ChannelData = data;
            var callparams = await context.SendActivityAsync(act);

            throw new NotImplementedException();
        }
    }
}
