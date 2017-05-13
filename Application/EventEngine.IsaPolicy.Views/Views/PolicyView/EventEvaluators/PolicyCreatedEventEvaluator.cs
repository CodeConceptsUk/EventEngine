using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.EventEvaluators
{
    public class PolicyCreatedEventEvaluator : IEventEvaluator<PolicyCreatedEvent, Contracts.Views.PolicyView.Domain.PolicyView>
    {
        public void Evaluate(Contracts.Views.PolicyView.Domain.PolicyView view, PolicyCreatedEvent @event)
        {
            view.PolicyNumber = @event.PolicyNumber;
            view.CustomerId = @event.CustomerId;
        }
    }
}