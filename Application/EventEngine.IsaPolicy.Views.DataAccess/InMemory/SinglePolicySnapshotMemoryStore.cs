﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Newtonsoft.Json;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Application.PropertyBags;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.DataAccess.InMemory
{
    public abstract class SnapshotMemoryStore<TView> : ISnapshotStore<TView>
        where TView : class, IView
    {
        private static readonly Dictionary<Guid, OrderedDictionary> Store = new Dictionary<Guid, OrderedDictionary>();

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public ISnapshot<TView> Get(Guid contextId)
        {
            if (!Store.ContainsKey(contextId))
                return null;

            var serializedView = Store[contextId].Values.OfType<string>().LastOrDefault();
            return serializedView == null
                ? null
                : Deserialize(serializedView);
        }

        public void Add(TView view, IEvent @event)
        {
            var contextId = @event.EventContextId;
            if (!Store.ContainsKey(contextId))
                Store[contextId] = new OrderedDictionary();

            var snapshot = new Snapshot<TView>(@event, view);
            CrudeSizeTrimmer(contextId);
            Store[contextId].Add(@event.EventId, Serialize(snapshot));
        }

        private static void CrudeSizeTrimmer(Guid contextId)
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

        private static string Serialize(Snapshot<TView> value)
        {
            return JsonConvert.SerializeObject(value, JsonSerializerSettings);
        }

        private static Snapshot<TView> Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Snapshot<TView>>(value, JsonSerializerSettings);
        }
    }

    public class SinglePolicySnapshotMemoryStore : SnapshotMemoryStore<PolicyView>
    {
        
    }

    public class SinglePolicyTransactionSnapshotMemoryStore : SnapshotMemoryStore<PolicyTransactionView>
    {

    }
}