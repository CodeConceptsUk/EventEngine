using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.PolicyFundUnitBalance
{
    public class AppliedFundChargeEventEvaluator : IEventEvaluator<AppliedFundChargeEvent, PolicyFundAllocationUnitBalanceView>
    {
        public void Evaluate(PolicyFundAllocationUnitBalanceView andLatestChargeDateView, AppliedFundChargeEvent @event)
        {
            var allocation = andLatestChargeDateView.FundAllocations.Single(alloc => alloc.FundId == @event.FundId && alloc.PortionId == @event.PortionId);
            allocation.UnitBalance += @event.Units;
            allocation.ChargesUpToDateOn = @event.ChargeDate;
        }
    }
}