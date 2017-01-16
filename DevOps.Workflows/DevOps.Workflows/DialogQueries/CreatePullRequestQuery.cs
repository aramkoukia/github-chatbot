using Microsoft.Bot.Builder.FormFlow;
using System;

namespace DevOps.Dialogs
{
    [Serializable]
    public class CreatePullRequestQuery
    {
        [Prompt("Please enter PR {&}")]
        public string Title { get; set; }

        [Prompt("What's the {&} branch? (The name of the branch you want to merge)")]
        public string Base { get; set; }

        [Prompt("What's the {&} branch? (The name of the branch you want to pull the change into)")]
        public string Head { get; set; }

    }
}
