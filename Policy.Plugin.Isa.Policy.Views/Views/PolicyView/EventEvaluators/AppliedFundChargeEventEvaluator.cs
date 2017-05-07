using System.Linq;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyView.EventEvaluators
{
    public class AppliedFundChargeEventEvaluator : IEventEvaluator<AppliedFundChargeEvent, Domain.PolicyView>
    {
        public void Evaluate(Domain.PolicyView view, AppliedFundChargeEvent @event)
        {
            var fund = view.Funds.Single(f => f.FundId == @event.FundId);
            //var allocation = fund.Allocations.Single(a => a.PortionId == @event.PortionId);
            //allocation.Units += @event.Units;
            //allocation.Charges.Add(new Charge{Units =@event.Units, Date = @event.ChargeDate});
            fund.TotalUnits += @event.Units;
        }
    }
}