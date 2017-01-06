using System;

namespace DevOps.Contracts
{
    [Serializable]
    public class CodeRepository
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Owner { get; set; }
        public string Url { get; set; }
    }
}