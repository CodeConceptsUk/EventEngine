using CodeConcepts.EventEngine.Application.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.EventEvaluators
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