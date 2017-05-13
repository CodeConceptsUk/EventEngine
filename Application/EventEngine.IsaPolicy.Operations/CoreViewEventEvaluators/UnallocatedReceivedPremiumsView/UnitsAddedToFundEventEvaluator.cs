using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.UnallocatedReceivedPremiumsView
{
    public class UnitsAddedToFundEventEvaluator : IEventEvaluator<UnitsAddedToFundEvent, Domain.PolicyView>
    {
        public void Evaluate(Domain.PolicyView view, UnitsAddedToFundEvent @event)
        {
            var allocation = new FundAllocation
            {
                Units = @event.Units,
                PortionId = @event.PortionId,
                ShadowUnits = @event.Units
            };
            var fund = view.Funds.SingleOrDefault(f => f.FundId == @event.FundId);
            if (fund == null)
            {
                fund = new Fund
                {
                    FundId = @event.FundId
                };
                view.Funds.Add(fund);
            }
            fund.Allocations.Add(allocation);
            fund.TotalUnits += @event.Units;
            fund.TotalShadowUnits += @event.Units;
        }
    }
}