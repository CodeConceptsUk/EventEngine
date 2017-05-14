using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatus;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.PremiumsStatus
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