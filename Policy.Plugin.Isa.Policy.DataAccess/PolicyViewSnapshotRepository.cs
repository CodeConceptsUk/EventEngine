using System;
using System.Collections.Generic;
using System.Linq;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Views;

namespace Policy.Plugin.Isa.Policy.DataAccess
{
    public class PolicyViewSnapshotRepository : ISnapshotRepository<IPolicyContext, PolicyView>
    {
        private static readonly Dictionary<Guid, SortedDictionary<DateTime, PolicyView>> _store =
            new Dictionary<Guid, SortedDictionary<DateTime, PolicyView>>();

        public SortedDictionary<DateTime, PolicyView> CreateSortedDictionary()
        {
            return new SortedDictionary<DateTime, PolicyView>(new ReverseDateComparer());
        }

        public PolicyView GetViewAtOrBefore(Guid eventContextId, DateTime expectedDateTime)
        {
            if (!_store.ContainsKey(eventContextId))
            {
                return null;
            }
            var store = _store[eventContextId];

            return store.ContainsKey(expectedDateTime) ? store[expectedDateTime] : (from key in store.Keys where key <= expectedDateTime select store[key]).FirstOrDefault();
        }

        public void SaveView(PolicyView view, Guid eventContextId, DateTime viewValidDateTime)
        {
            if (!_store.ContainsKey(eventContextId))
            {
                _store[eventContextId] = CreateSortedDictionary();
            }
            _store[eventContextId].Add(viewValidDateTime, view);
        }

        private class ReverseDateComparer : IComparer<DateTime>
        {
            public int Compare(DateTime x, DateTime y)
            {
                return 0-DateTime.Compare(x, y);
            }
        }
    }
}