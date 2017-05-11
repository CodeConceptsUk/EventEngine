using System;
using System.Linq;
using CodeConcepts.EventEngine.Application.Interfaces;
using CodeConcepts.EventEngine.Application.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.DataAccess.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries
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