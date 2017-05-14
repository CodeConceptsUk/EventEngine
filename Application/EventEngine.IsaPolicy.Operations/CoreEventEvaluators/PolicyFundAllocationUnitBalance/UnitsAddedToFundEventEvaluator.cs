using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.PolicyFundUnitBalance
{
    public class UnitsAddedToFundEventEvaluator : IEventEvaluator<UnitsAddedToFundEvent, PolicyFundAllocationUnitBalanceView>
    {
        public void Evaluate(PolicyFundAllocationUnitBalanceView andLatestChargeDateView, UnitsAddedToFundEvent @event)
        {
            var allocation = new FundAllocationUnitBalance
            {
                UnitBalance = @event.Units,
                PortionId = @event.PortionId,
                FundId = @event.FundId,
                ChargesUpToDateOn = @event.DateTimeAdded
            };
            andLatestChargeDateView.FundAllocations.Add(allocation);
        }
    }
}