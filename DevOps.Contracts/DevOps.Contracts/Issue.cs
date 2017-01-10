using System;

namespace DevOps.Contracts
{
    [Serializable]
    public class Issue
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string State { get; set; }
        public string Repository { get; set; }
    }
}
