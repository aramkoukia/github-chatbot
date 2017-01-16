using DevOps.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevOps.Github
{
    public class DataMapper
    {
        public static IEnumerable<CodeRepository> Map(List<GithubRepositoryDto> result)
        {
            if (result == null)
                return null;

            return result.Select(a => new CodeRepository
            {
                FullName = a.full_name,
                Id = a.id.ToString(),
                Name = a.name,
                Owner = a.owner.login,
                Url = a.url
            }).ToList();
        }

        public static Issue Map(GithubIssueDto result)
        {
            if (result == null)
                return null;

            return new Issue
            {
                Body = result.body,
                Number = result.id.ToString(),
                Repository = result.repository_url,
                State = result.state,
                Title = result.title
            };
        }
    }
}
