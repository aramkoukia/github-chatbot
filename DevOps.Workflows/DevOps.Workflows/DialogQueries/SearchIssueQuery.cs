using Microsoft.Bot.Builder.FormFlow;
using System;

namespace DevOps.Dialogs
{
    [Serializable]
    public class SearchIssueQuery
    {
        [Prompt("Please enter issue {&}")]
        public string ID { get; set; }

        [Prompt("Search in which {&}?")]
        public string Repository { get; set; }

    }
}
