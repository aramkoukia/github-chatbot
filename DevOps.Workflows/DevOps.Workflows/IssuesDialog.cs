using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using DevOps.Contracts;
using DevOps.Dialogs;
using System.Web;

namespace DevOps.Dialogs
{
    [Serializable]
    public class IssuesDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to issues manager!");

            var hotelsFormDialog = FormDialog.FromForm(BuildHotelsForm, FormOptions.PromptInStart);

            context.Call(hotelsFormDialog, ResumeAfterHotelsFormDialog);
        }

        private IForm<IssuesQuery> BuildHotelsForm()
        {
            OnCompletionAsyncDelegate<IssuesQuery> processHotelsSearch = async (context, state) =>
            {
                await context.PostAsync($"Ok. Searching for Hotels in {state.Title} from to ...");
            };

            return new FormBuilder<IssuesQuery>()
                .Field(nameof(IssuesQuery.Title))
                .Message("Looking for hotels in {Destination}...")
                .AddRemainingFields()
                .OnCompletion(processHotelsSearch)
                .Build();
        }

        private async Task ResumeAfterHotelsFormDialog(IDialogContext context, IAwaitable<IssuesQuery> result)
        {
            try
            {
                var searchQuery = await result;

                var issues = await GetHotelsAsync(searchQuery);

                await context.PostAsync($"I found in total {issues.Count()} hotels for your dates:");

                var resultMessage = context.MakeMessage();
                resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                resultMessage.Attachments = new List<Attachment>();

                foreach (var issue in issues)
                {
                    HeroCard heroCard = new HeroCard()
                    {
                        Title = "sdff",
                        Subtitle = "sdf",
                        Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "More details",
                                Type = ActionTypes.OpenUrl,
                                Value = $"https://www.bing.com/search?q=hotels+in+"
                            }
                        }
                    };

                    resultMessage.Attachments.Add(heroCard.ToAttachment());
                }

                await context.PostAsync(resultMessage);
            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation. Quitting from the HotelsDialog";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }
            finally
            {
                context.Done<object>(null);
            }
        }

        private async Task<IEnumerable<Issue>> GetHotelsAsync(IssuesQuery searchQuery)
        {
            var issues = new List<Issue>();

            // Filling the hotels results manually just for demo purposes
            for (int i = 1; i <= 5; i++)
            {
                var random = new Random(i);
                Issue hotel = new Issue()
                {
                    //Name = $"{searchQuery.Destination} Hotel {i}",
                    //Location = searchQuery.Destination,
                    //Rating = random.Next(1, 5),
                    //NumberOfReviews = random.Next(0, 5000),
                    //PriceStarting = random.Next(80, 450),
                    //Image = $"https://placeholdit.imgix.net/~text?txtsize=35&txt=Hotel+{i}&w=500&h=260"
                };

                issues.Add(hotel);
            }

            //issues.Sort((h1, h2) => h1.PriceStarting.CompareTo(h2.PriceStarting));

            return issues;
        }
    }
}