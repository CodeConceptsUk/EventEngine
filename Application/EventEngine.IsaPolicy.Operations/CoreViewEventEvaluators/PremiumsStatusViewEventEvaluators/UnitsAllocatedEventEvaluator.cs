using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatusView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PremiumsStatusViewEventEvaluators
{
    public class UnitsAllocatedEventEvaluator : IEventEvaluator<UnitsAllocatedEvent, PremiumsStatusView>
    {
        public void Evaluate(PremiumsStatusView view, UnitsAllocatedEvent @event)
        {
            var premiumId = @event.PremiumId;
            view.ReceivedPremiumIds.Remove(premiumId);
            view.AllocatedPremiumIds.Add(premiumId);
        }
    }
}