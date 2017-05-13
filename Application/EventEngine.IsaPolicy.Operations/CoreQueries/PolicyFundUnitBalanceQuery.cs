using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalanceView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueries
{
    public class PolicyFundUnitBalanceQuery : PolicyIndexedQuery<PolicyFundUnitBalanceView>, IPolicyFundUnitBalanceQuery
    {
        
        public PolicyFundUnitBalanceQuery(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<PolicyFundUnitBalanceView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
            :base(eventStore, snapshotStore, player)
        {
            
        }
    }
}