using System;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatusView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueries
{
    public class PendingPremiumQuery : IPremiumStatusQuery
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;
        private readonly ISnapshotStore<PremiumsStatusView> _snapshotStoreStore;
        private readonly IEventPlayer<IsaPolicyEvent> _player;

        public PendingPremiumQuery(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<PremiumsStatusView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }
        
        public PremiumsStatusView Read(Guid contextId)
        {
            var snapshot = _snapshotStoreStore.Get(contextId);
            
            var events = _eventStore.Get(contextId, snapshot?.Event.EventId);
            if (!events.Any())
                return snapshot?.View;

            var view = _player.Handle(events, snapshot?.View ?? new PremiumsStatusView());

            _snapshotStoreStore.Add(view, events.Last());

            return view;
        }
    }
}