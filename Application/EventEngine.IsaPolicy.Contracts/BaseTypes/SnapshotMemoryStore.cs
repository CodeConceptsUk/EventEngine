using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.Contracts.PropertyBags;
using Newtonsoft.Json;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes
{
    public abstract class SnapshotMemoryStore<TView> : ISnapshotStore<TView>
        where TView : class, IView
    {
        private readonly Dictionary<Guid, OrderedDictionary> _store = new Dictionary<Guid, OrderedDictionary>();

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public ISnapshot<TView> Get(Guid contextId)
        {
            if (!_store.ContainsKey(contextId))
                return null;

            var serializedView = _store[contextId].Values.OfType<string>().LastOrDefault();
            return serializedView == null
                ? null
                : Deserialize(serializedView);
        }

        public void Add(TView view, IEvent @event)
        {
            var contextId = @event.EventContextId;
            if (!_store.ContainsKey(contextId))
                _store[contextId] = new OrderedDictionary();

            var snapshot = new Snapshot<TView>(@event, view);
            CrudeSizeTrimmer(contextId);
            _store[contextId].Add(@event.EventId, Serialize(snapshot));
        }

        private void CrudeSizeTrimmer(Guid contextId)
        {
            // clear out all old snapshots regularly.
            if (_store[contextId].Count > 15)
            {
                _store[contextId].Clear();
            }
        }

        public void ClearAllSnapshots()
        {
            var keys = _store.Keys.ToArray();
            foreach (var guid in keys)
            {
                _store[guid].Clear();
            }
        }

        private string Serialize(Snapshot<TView> value)
        {
            return JsonConvert.SerializeObject(value, _jsonSerializerSettings);
        }

        private Snapshot<TView> Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Snapshot<TView>>(value, _jsonSerializerSettings);
        }
    }
}
