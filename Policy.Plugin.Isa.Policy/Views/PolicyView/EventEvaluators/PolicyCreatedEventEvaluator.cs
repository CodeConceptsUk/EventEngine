using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;

namespace Policy.Plugin.Isa.Policy.Views.PolicyView.EventEvaluators
{
    public class PolicyCreatedEventEvaluator : IEventEvaluator<PolicyCreatedEvent, PolicyView>
    {
        public void Evaluate(PolicyView view, PolicyCreatedEvent @event)
        {
            view.PolicyNumber = @event.PolicyNumber;
            view.CustomerId = @event.CustomerId;
        }
    }
}