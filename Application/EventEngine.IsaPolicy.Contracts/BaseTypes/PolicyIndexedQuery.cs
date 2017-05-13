// ReSharper disable PossibleMultipleEnumeration

using System;
using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes
{
    public abstract class PolicyIndexedQuery<TView>
        where TView : class, IView, new()
    {

        private readonly IIsaPolicyEventStoreRepository _eventStore;
        private readonly ISnapshotStore<TView> _snapshotStoreStore;
        private readonly IEventPlayer<IsaPolicyEvent> _player;

        protected PolicyIndexedQuery(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<TView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }

        public TView Read(Guid contextId)
        {
            var snapshot = _snapshotStoreStore.Get(contextId); //   snapshot => snapshot.)

            var events = _eventStore.Get(contextId, snapshot?.Event.EventId);
            if (!events.Any())
                return snapshot?.View;

            var view = _player.Handle(events, snapshot?.View ?? new TView());

            _snapshotStoreStore.Add(view, events.Last());

            return view;
        }
    }
}