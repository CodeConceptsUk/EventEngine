using Application.Events;
using Application.Interfaces;
using Application.Interfaces.Domain;
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
}