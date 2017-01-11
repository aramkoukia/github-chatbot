using Microsoft.Bot.Builder.FormFlow;
using System;

namespace DevOps.Dialogs
{
    [Serializable]
    public class GetIssueQuery
    {
        [Prompt("Please enter issue {&}")]
        public string Id { get; set; }

        [Prompt("Search in which {&}?")]
        public string Repository { get; set; }

    }
}
