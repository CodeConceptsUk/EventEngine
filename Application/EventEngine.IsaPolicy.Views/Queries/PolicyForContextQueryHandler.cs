using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView.Domain;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries
{
    public class PolicyForContextQueryHandler : IQueryHandler<GetPolicyForContextIdQuery, PolicyView> 
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;
        private readonly ISnapshotStore<PolicyView> _snapshotStoreStore;
        private readonly IEventPlayer<IsaPolicyEvent> _player;

        public PolicyForContextQueryHandler(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<PolicyView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }

        public PolicyView Read(GetPolicyForContextIdQuery forContextIdQuery)
        {
            var contextId = forContextIdQuery.ContextId;
            var snapshot = _snapshotStoreStore.Get(contextId); //   snapshot => snapshot.)

            var events = _eventStore.Get(contextId, snapshot?.Event.EventId);
            if (!events.Any())
                return snapshot?.View;

            var view = _player.Handle(events, snapshot?.View ?? new PolicyView());

            _snapshotStoreStore.Add(view, events.Last());

            return view;
        }
    }
}