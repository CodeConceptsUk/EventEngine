using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatusView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PremiumsStatusViewEventEvaluators
{
    public class PremiumReceivedEventEvaluator : IEventEvaluator<PremiumReceivedEvent, PremiumsStatusView>
    {
        public void Evaluate(PremiumsStatusView view, PremiumReceivedEvent @event)
        {
            var premiumId = @event.PremiumId;
            view.PendingPremiumIds.Remove(premiumId);
            view.ReceivedPremiumIds.Add(premiumId);
        }
    }
}