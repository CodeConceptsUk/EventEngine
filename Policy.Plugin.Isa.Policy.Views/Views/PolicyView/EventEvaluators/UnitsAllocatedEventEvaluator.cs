using System.Linq;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyView.EventEvaluators
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