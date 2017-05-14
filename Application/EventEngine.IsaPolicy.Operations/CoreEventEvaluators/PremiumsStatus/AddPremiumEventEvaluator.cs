using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatus;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.PremiumsStatus
{
    public class AddPremiumEventEvaluator : IEventEvaluator<AddPremiumEvent, PremiumsStatusView>
    {
        public void Evaluate(PremiumsStatusView view, AddPremiumEvent @event)
        {
            view.PendingPremiumIds.Add(@event.PremiumId);
        }
    }
}