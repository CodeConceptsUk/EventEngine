using System;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.DataAccess.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueries
{
    public class UnallocatedReceivedPremiumsQuery : IUnallocatedReceivedPremiumsQuery
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;
        private readonly ISnapshotStore<UnallocatedReceivedPremiumsView> _snapshotStoreStore;
        private readonly IEventPlayer<IsaPolicyEvent> _player;

        public UnallocatedReceivedPremiumsQuery(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<UnallocatedReceivedPremiumsView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }

        public UnallocatedReceivedPremiumsView Read(Guid contextId)
        {
            var snapshot = _snapshotStoreStore.Get(contextId); //   snapshot => snapshot.)

            var events = _eventStore.Get(contextId, snapshot?.Event.EventId);
            if (!events.Any())
                return snapshot?.View;

            var view = _player.Handle(events, snapshot?.View ?? new UnallocatedReceivedPremiumsView());

            _snapshotStoreStore.Add(view, events.Last());

            return view;
        }
    }
}