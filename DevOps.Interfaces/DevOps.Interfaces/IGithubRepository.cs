﻿using DevOps.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevOps.Interfaces
{
    public interface IGithubRepository
    {
        Task<IEnumerable<CodeRepository>> GetCodeRepositories();
    }
}