using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueryHandlers
{
    public class PolicyFundUnitBalanceQueryHandler : EventContextIdIndexedQueryHandler<GetPolicyFundUnitBalanceQuery, PolicyFundUnitBalanceView>
    {
        
        public PolicyFundUnitBalanceQueryHandler(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<PolicyFundUnitBalanceView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
            :base(eventStore, snapshotStore, player)
        {
            
        }
    }
}