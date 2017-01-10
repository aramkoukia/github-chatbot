using Microsoft.Bot.Builder.FormFlow;
using System;

namespace DevOps.Dialogs
{
    [Serializable]
    public class CreateIssueQuery
    {
        [Prompt("Please enter issue {&}")]
        public string Title { get; set; }

        [Prompt("Create this in which {&}?")]
        public string Repository { get; set; }

        [Prompt("Enter more detail for the {&}?")]
        public string Body { get; set; }
    }
}
