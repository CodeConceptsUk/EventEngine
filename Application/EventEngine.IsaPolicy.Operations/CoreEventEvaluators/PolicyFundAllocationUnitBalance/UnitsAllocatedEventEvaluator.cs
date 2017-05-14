using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.PolicyFundUnitBalance
{
    public class UnitsAllocatedEventEvaluator : IEventEvaluator<UnitsAllocatedEvent, PolicyFundAllocationUnitBalanceView>
    {
        public void Evaluate(PolicyFundAllocationUnitBalanceView andLatestChargeDateView, UnitsAllocatedEvent @event)
        {
            andLatestChargeDateView.FundAllocations.Add(new FundAllocationUnitBalance
            {
                PortionId = @event.PortionId,
                UnitBalance = @event.Units,
                FundId = @event.FundId,
                ChargesUpToDateOn = @event.AllocationDateTime
            });
        }
    }
}