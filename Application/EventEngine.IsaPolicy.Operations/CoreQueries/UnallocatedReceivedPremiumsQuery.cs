using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiumsView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueries
{
    public class UnallocatedReceivedPremiumsQuery : PolicyIndexedQuery<UnallocatedReceivedPremiumsView>, IUnallocatedReceivedPremiumsQuery
    {

        public UnallocatedReceivedPremiumsQuery(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<UnallocatedReceivedPremiumsView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
            : base(eventStore, snapshotStore, player)
        {

        }
    }
}