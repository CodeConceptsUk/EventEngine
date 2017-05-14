using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatus;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.PremiumsStatus
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