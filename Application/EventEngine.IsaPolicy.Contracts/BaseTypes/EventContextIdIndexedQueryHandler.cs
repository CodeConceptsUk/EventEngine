using System.Linq;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes
{
    public abstract class EventContextIdIndexedQueryHandler<TQuery, TView> : IQueryHandler<TQuery, TView>
        where TView : class, IView, new() 
        where TQuery : class, IEventContextIdQuery
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;
        private readonly ISnapshotStore<TView> _snapshotStoreStore;
        private readonly IEventPlayer<IsaPolicyEvent> _player;

        protected EventContextIdIndexedQueryHandler(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<TView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }
        
        public TView Read(TQuery query)
        {
            var snapshot = _snapshotStoreStore.Get(query.EventContextId);

            var events = _eventStore.Get(query.EventContextId, snapshot?.Event.EventId);
            if (!events.Any())
                return snapshot?.View;

            var view = _player.Handle(events, snapshot?.View ?? new TView());

            _snapshotStoreStore.Add(view, events.Last());

            return view;
        }
    }
}