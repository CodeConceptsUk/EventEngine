using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Newtonsoft.Json;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Application.PropertyBags;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.DataAccess.InMemory
{
    public class SinglePolicySnapshotMemoryStore : ISnapshotStore<PolicyView>
    {
        private readonly IEventStoreRepository<IsaPolicyEvent> _eventStore;
        private static readonly Dictionary<Guid, OrderedDictionary> Store = new Dictionary<Guid, OrderedDictionary>();

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        
        public ISnapshot<PolicyView> Get(Guid contextId)
        {
            if (!Store.ContainsKey(contextId))
                return null;

            var serializedView = Store[contextId].Values.OfType<string>().Last();
            return Deserialize(serializedView);
        }

        public void Add(PolicyView view, IEvent @event)
        {
            var contextId = @event.EventContextId;
            if (!Store.ContainsKey(contextId))
                Store[contextId] = new OrderedDictionary();
            
            var snapshot = new Snapshot<PolicyView>(@event, view);
            CrudeSizeTrimmer(contextId);
            Store[contextId].Add(@event.EventId, Serialize(snapshot));
        }

        private void CrudeSizeTrimmer(Guid contextId)
        {
            // clear out all old snapshots regularly.
            if (Store[contextId].Count > 15)
            {
                Store[contextId].Clear();
            }
        }

        public void ClearAllSnapshots()
        {
            var keys = Store.Keys.ToArray();
            foreach (var guid in keys)
            {
                Store[guid].Clear();
            }
        }

        private string Serialize(Snapshot<PolicyView> value)
        {
            return JsonConvert.SerializeObject(value, JsonSerializerSettings);
        }

        private Snapshot<PolicyView> Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Snapshot<PolicyView>>(value, JsonSerializerSettings);
        }
    }
}