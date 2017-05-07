using System;
using System.Linq;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Views.Queries;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Domain;

// ReSharper disable PossibleMultipleEnumeration

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyTransactionsView.Queries
{
    public class SinglePolicyTransactionQuery : ISinglePolicyTransactionQuery
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;
        private readonly ISnapshotStore<PolicyTransactionView> _snapshotStoreStore;
        private readonly IEventPlayer<IsaPolicyEvent> _player;

        public SinglePolicyTransactionQuery(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<PolicyTransactionView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }

        public PolicyTransactionView Read(Guid contextId)
        {
            var snapshot = _snapshotStoreStore.Get(contextId); //   snapshot => snapshot.)

            var events = _eventStore.Get(contextId, snapshot?.Event.EventId);
            if (!events.Any())
                return snapshot?.View;

            var view = _player.Handle(events, snapshot?.View ?? new PolicyTransactionView());

            _snapshotStoreStore.Add(view, events.Last());

            return view;
        }
    }
}