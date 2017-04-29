using System.Collections.Generic;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;

// ReSharper disable CollectionNeverUpdated.Local

namespace Policy.Plugin.Isa.Policy.DataAccess
{
    public class SequencingRepository : ISequencingRepository
    {
        readonly Dictionary<string, int> _numbers = new Dictionary<string, int>();
        
        public string Get(string type)
        {
            if (!_numbers.ContainsKey(type))
                _numbers.Add(type, 1);

            return (_numbers[type]++).ToString();
        }
    }
}