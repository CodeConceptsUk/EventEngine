using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PolicyFundBalanceView
{
    public class PremiumReceivedEventEvaluator : IEventEvaluator<PremiumReceivedEvent, Domain.PolicyView>
    {
        public void Evaluate(Domain.PolicyView view, PremiumReceivedEvent @event)
        {
            view.Premiums.Single(p => p.PremiumId == @event.PremiumId).IsReceived = true;
        }
    }
}