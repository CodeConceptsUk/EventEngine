using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyView.EventEvaluators
{
    public class PolicyCreatedEventEvaluator : IEventEvaluator<PolicyCreatedEvent, Domain.PolicyView>
    {
        public void Evaluate(Domain.PolicyView view, PolicyCreatedEvent @event)
        {
            view.PolicyNumber = @event.PolicyNumber;
            view.CustomerId = @event.CustomerId;
        }
    }
}