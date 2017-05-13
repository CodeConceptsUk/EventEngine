using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalanceView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PolicyFundUnitBalanceViewEventEvaluators
{
    public class AppliedFundChargeEventEvaluator : IEventEvaluator<AppliedFundChargeEvent, PolicyFundUnitBalanceView>
    {
        public void Evaluate(PolicyFundUnitBalanceView view, AppliedFundChargeEvent @event)
        {
            var allocation = view.FundAllocations.Single(alloc => alloc.FundId == @event.FundId && alloc.PortionId == @event.PortionId);
            allocation.UnitBalance += @event.Units;
        }
    }
}