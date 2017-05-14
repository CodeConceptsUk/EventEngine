using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.PolicyFundUnitBalance
{
    public class PolicyCreatedEventEvaluator : IEventEvaluator<PolicyCreatedEvent, PolicyFundAllocationUnitBalanceView>
    {
        public void Evaluate(PolicyFundAllocationUnitBalanceView andLatestChargeDateView, PolicyCreatedEvent @event)
        {
        }
    }
}