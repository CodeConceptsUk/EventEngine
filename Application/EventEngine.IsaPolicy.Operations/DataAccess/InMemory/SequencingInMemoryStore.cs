using System.Collections.Generic;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

// ReSharper disable CollectionNeverUpdated.Local

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.InMemory
{
    public class SequencingInMemoryStore : ISequencingRepository
    {
        private readonly Dictionary<string, int> _numbers = new Dictionary<string, int>();
        
        public string Get(string type)
        {
            if (!_numbers.ContainsKey(type))
                _numbers.Add(type, 1);

            return (_numbers[type]++).ToString();
        }
    }
}