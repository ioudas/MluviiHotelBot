﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json.Linq;

namespace BasicBot.Dialogs.CheckIn
{
    public class CheckInDialog : ComponentDialog
    {
        // Prompts names
        private const string CardPrompt = "cardPrompt";
        
        private const int CardIdLengthMinValue = 6;

        // Dialog IDs
        private const string CheckInDialogId = "checkInDialog";

        public CheckInDialog()
            : base(nameof(CheckInDialog))
        {

            // Add control flow dialogs
            var waterfallSteps = new WaterfallStep[]
            {
                    PromptForCardStepAsync,
                    DisplayGreetingStateStepAsync,
            };
            AddDialog(new WaterfallDialog(CheckInDialogId, waterfallSteps));
            AddDialog(new TextPrompt(CardPrompt));
        }

        private async Task<DialogTurnResult> PromptForCardStepAsync(
                                                WaterfallStepContext stepContext,
                                                CancellationToken cancellationToken)
        {
            var kek = stepContext.Result;
            var opts = new PromptOptions
            {
                Prompt = new Activity
                {
                    Type = ActivityTypes.Message,
                    Text = "Send us Your ID photo, please. Opening prompt...",
                },
            };
            var data = JObject.Parse(@"{ ""Activity"": ""ChatbotOpenFileUploadPrompt"" }");
            var act = stepContext.Context.Activity.CreateReply();
            act.ChannelData = data;
            await stepContext.Context.SendActivityAsync(act);

            return await stepContext.PromptAsync(CardPrompt, opts);
        }
        private async Task<DialogTurnResult> DisplayGreetingStateStepAsync(
                                                    WaterfallStepContext stepContext,
                                                    CancellationToken cancellationToken)
        {
            return await GreetUser(stepContext);
        }

        // Helper function to greet user with information in GreetingState.
        private async Task<DialogTurnResult> GreetUser(WaterfallStepContext stepContext)
        {
            var context = stepContext.Context;

            // Display their profile information and end dialog.
            await context.SendActivityAsync($"Thank you! This is Your room key 123123, nice to meet you!");
            return await stepContext.EndDialogAsync();
        }
    }
}
