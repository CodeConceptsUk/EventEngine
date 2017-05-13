using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PremiumStatusView
{
    public class UnitsAllocatedEventEvaluator : IEventEvaluator<UnitsAllocatedEvent, Domain.PolicyView>
    {
        public void Evaluate(Domain.PolicyView view, UnitsAllocatedEvent @event)
        {
            var premium = view.Premiums.Single(p => p.PremiumId == @event.PremiumId);
            var partition = premium.Partitions.Single(p => p.PortionId == @event.PortionId);

            var fund = view.Funds.SingleOrDefault(f => f.FundId == @event.FundId);
            if (fund == null)
            {
                fund = new Fund
                {
                    FundId = @event.FundId
                };
                view.Funds.Add(fund);
            }
            fund.Allocations.Add(new FundAllocation
            {
                PortionId = @event.PortionId,
                PremiumPartition = partition,
                Units = @event.Units,
                ShadowUnits = @event.Units
            });
            fund.TotalUnits += @event.Units;
            fund.TotalShadowUnits += @event.Units;
            premium.IsAllocated = true;
        }
    }
}