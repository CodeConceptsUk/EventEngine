using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.PolicyFundUnitBalance
{
    public class AddPremiumEventEvaluator : IEventEvaluator<AddPremiumEvent, PolicyFundAllocationUnitBalanceView>
    {
        public void Evaluate(PolicyFundAllocationUnitBalanceView andLatestChargeDateView, AddPremiumEvent @event)
        {
        }
    }
}