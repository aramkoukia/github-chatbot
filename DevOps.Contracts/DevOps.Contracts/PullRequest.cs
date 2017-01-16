using System;

namespace DevOps.Contracts
{
    [Serializable]
    public class PullRequest
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string State { get; set; }
        public string Head { get; set; }
        public string Base { get; set; }
        public string Repository { get; set; }
        public bool MaintainerCanModify { get; set; }
    }
}
