using System.Linq;
using Application.Events;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.PropertyBags;
using Application.Views;

namespace Application.EventEvaluators
{
    public class PolicyCreatedEventEvaluator : IEventEvaluator<PolicyCreatedEvent, IPolicyContext, PolicyView>
    {
        public void Evaluate(PolicyView view, PolicyCreatedEvent @event)
        {
            view.PolicyNumber = @event.PolicyNumber;
            view.CustomerId = @event.CustomerId;
        }
    }

    public class AddPremiumEventEvaluator : IEventEvaluator<AddPremiumEvent, IPolicyContext, PolicyView>
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