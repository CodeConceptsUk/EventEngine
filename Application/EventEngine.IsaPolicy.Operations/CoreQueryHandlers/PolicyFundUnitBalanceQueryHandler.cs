using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueryHandlers
{
    public class PolicyFundUnitBalanceQueryHandler : EventContextIdIndexedQueryHandler<GetPolicyFundUnitBalanceQuery, PolicyFundAllocationUnitBalanceView>, IPolicyFundUnitBalanceQueryHandler
    {
        
        public PolicyFundUnitBalanceQueryHandler(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<PolicyFundAllocationUnitBalanceView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
            : base(eventStore, snapshotStore, player)
        {
            
        }
    }
}