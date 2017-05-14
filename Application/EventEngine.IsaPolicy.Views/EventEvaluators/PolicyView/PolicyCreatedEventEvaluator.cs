using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.EventEvaluators.PolicyView
{
    public class PolicyCreatedEventEvaluator : IEventEvaluator<PolicyCreatedEvent, Contracts.Views.PolicyView.PolicyView>
    {
        public void Evaluate(Contracts.Views.PolicyView.PolicyView view, PolicyCreatedEvent @event)
        {
            view.PolicyNumber = @event.PolicyNumber;
            view.CustomerId = @event.CustomerId;
        }
    }
}