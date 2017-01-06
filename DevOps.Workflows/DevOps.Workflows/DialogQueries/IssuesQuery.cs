using Microsoft.Bot.Builder.FormFlow;
using System;

namespace DevOps.Dialogs
{
    [Serializable]
    public class IssuesQuery
    {
        [Prompt("Please enter issue {&}")]
        public string Title { get; set; }

        [Prompt("Enter more detail for the {&}?")]
        public string Body { get; set; }

        //[Numeric(1, int.MaxValue)]
        //[Prompt("How many {&} do you want to stay?")]
        //public int Nights { get; set; }
    }
}
