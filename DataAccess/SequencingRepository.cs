using System.Collections.Generic;
using Application.Interfaces.Repositories;
// ReSharper disable CollectionNeverUpdated.Local

namespace DataAccess
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