using System.Linq;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.PropertyBags;

namespace Policy.Plugin.Isa.Policy.Views.PolicyView.EventEvaluators
{
    public class AddPremiumEventEvaluator : IEventEvaluator<AddPremiumEvent, PolicyView>
    {
        public void Evaluate(PolicyView view, AddPremiumEvent @event)
        {
            var fund = view.Funds.FirstOrDefault(t => t.FundId == @event.FundId);
            if (fund == null)
            {
                fund = new Fund(@event.FundId);
                view.Funds.Add(fund);
            }

            fund.UnallocatedPremiums += @event.Premium;
        }
    }
}