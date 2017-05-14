using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatus;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueryHandlers
{
    public class PremiumsStatusQueryHandler : EventContextIdIndexedQueryHandler<GetPremiumsStatusQuery, PremiumsStatusView>
    {
        public PremiumsStatusQueryHandler(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<PremiumsStatusView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
            : base(eventStore, snapshotStore, player)
        {
        }
    }
}