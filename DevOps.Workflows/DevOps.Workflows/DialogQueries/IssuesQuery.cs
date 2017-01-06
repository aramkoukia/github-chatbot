using Microsoft.Bot.Builder.FormFlow;
using System;

namespace DevOps.Dialogs
{
    [Serializable]
    public class IssuesQuery
    {
        [Prompt("Please enter issue title {&}")]
        public string Title { get; set; }

        [Prompt("Enter more detail for the body {&}?")]
        public DateTime Body { get; set; }

        //[Numeric(1, int.MaxValue)]
        //[Prompt("How many {&} do you want to stay?")]
        //public int Nights { get; set; }
    }
}
