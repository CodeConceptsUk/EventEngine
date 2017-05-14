using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiums;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueryHandlers
{
    public class UnallocatedReceivedPremiumsQueryHandler : EventContextIdIndexedQueryHandler<GetUnallocatedReceivedPremiumsQuery, UnallocatedReceivedPremiumsView>
    {
        public UnallocatedReceivedPremiumsQueryHandler(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<UnallocatedReceivedPremiumsView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
            : base(eventStore, snapshotStore, player)
        {}
    }
}