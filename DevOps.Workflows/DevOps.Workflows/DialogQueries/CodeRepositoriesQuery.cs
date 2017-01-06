using Microsoft.Bot.Builder.FormFlow;
using System;

namespace DevOps.Dialogs
{
    [Serializable]
    public class CodeRepositoriesQuery
    {
        [Prompt("Please enter repository name {&}")]
        public string Name { get; set; }
    }
}
