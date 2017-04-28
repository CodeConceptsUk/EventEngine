using System.Linq;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Views;

namespace Policy.Plugin.Isa.Policy.EventEvaluators
{
    public class AppliedFundChargeEventEvaluator : IEventEvaluator<AppliedFundChargeEvent, IPolicyContext, PolicyView>
    {
        public void Evaluate(PolicyView view, AppliedFundChargeEvent @event)
        {
            
        }
    }

    public class UnitsAllocatedEventEvaluator : IEventEvaluator<UnitsAllocatedEvent, IPolicyContext, PolicyView>
    {
        public void Evaluate(PolicyView view, UnitsAllocatedEvent @event)
        {
            var fund = view.Funds.First(f => f.FundId == @event.FundId);
            fund.UnallocatedPremiums -= @event.UsedPremium;
            fund.Units += @event.Units;
        }
    }
}