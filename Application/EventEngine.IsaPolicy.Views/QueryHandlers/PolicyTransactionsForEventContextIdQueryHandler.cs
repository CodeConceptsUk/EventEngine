using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeConcepts.EventEngine.IsaPolicy.Views.QueryHandlers
{
    public class PolicyTransactionsForEventContextIdQueryHandler : IQueryHandler<GetPolicyTransactionsForEventContextIdQuery, PolicyTransactionView>
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;
        private readonly ISnapshotStore<PolicyTransactionView> _snapshotStoreStore;
        private readonly IEventPlayer<IsaPolicyEvent> _player;

        public PolicyTransactionsForEventContextIdQueryHandler(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<PolicyTransactionView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }

        public PolicyTransactionView Read(GetPolicyTransactionsForEventContextIdQuery getPolicyTransactionsForEventContextIdQuery)
        {
            var snapshot = _snapshotStoreStore.Get(getPolicyTransactionsForEventContextIdQuery.ContextId);

            var events = _eventStore.Get(getPolicyTransactionsForEventContextIdQuery.ContextId, snapshot?.Event.EventId);
            if (!events.Any())
                return snapshot?.View;

            var view = _player.Handle(events, snapshot?.View ?? new PolicyTransactionView());

            _snapshotStoreStore.Add(view, events.Last());

            return view;
        }
    }
}