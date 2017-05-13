using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalanceView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PolicyFundUnitBalanceViewEventEvaluators
{
    public class UnitsAllocatedEventEvaluator : IEventEvaluator<UnitsAllocatedEvent, PolicyFundUnitBalanceView>
    {
        public void Evaluate(PolicyFundUnitBalanceView view, UnitsAllocatedEvent @event)
        {
            view.FundAllocations.Add(new FundAllocationBalance()
            {
                PortionId = @event.PortionId,
                UnitBalance = @event.Units,
                FundId = @event.FundId
            });
        }
    }
}