using DevOps.Contracts;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevOps.Interfaces
{
    public interface IDevelopementWorkflows
    {
        Task<IEnumerable<CodeRepository>> GetCodeRepositories(Activity activity);
    }
}
