using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.EventEvaluators.PolicyView
{
    public class PremiumReceivedEventEvaluator : IEventEvaluator<PremiumReceivedEvent, Contracts.Views.PolicyView.PolicyView>
    {
        public void Evaluate(Contracts.Views.PolicyView.PolicyView view, PremiumReceivedEvent @event)
        {
            view.Premiums.Single(p => p.PremiumId == @event.PremiumId).IsReceived = true;
        }
    }
}