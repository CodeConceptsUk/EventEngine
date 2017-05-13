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
    public class SinglePolicyFromContextQueryHandler : IQueryHandler<GetSinglePolicyFromContextQuery, PolicyView> 
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;
        private readonly ISnapshotStore<PolicyView> _snapshotStoreStore;
        private readonly IEventPlayer<IsaPolicyEvent> _player;

        public SinglePolicyFromContextQueryHandler(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<PolicyView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }

        public PolicyView Read(GetSinglePolicyFromContextQuery fromContextQuery)
        {
            var contextId = fromContextQuery.ContextId;
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