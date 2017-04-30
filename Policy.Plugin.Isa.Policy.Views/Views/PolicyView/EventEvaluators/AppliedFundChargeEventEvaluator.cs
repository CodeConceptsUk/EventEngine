using System.Linq;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;

namespace Policy.Plugin.Isa.Policy.Views.PolicyView.EventEvaluators
{
    public class AppliedFundChargeEventEvaluator : IEventEvaluator<AppliedFundChargeEvent, Domain.PolicyView>
    {
        public void Evaluate(Domain.PolicyView view, AppliedFundChargeEvent @event)
        {
            //var fund = view.Funds.First(f => f.FundId == @event.FundId);
            //fund.Units += @event.Units;
        }
    }
}