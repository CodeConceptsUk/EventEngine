using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalanceView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PolicyFundUnitBalanceViewEventEvaluators
{
    public class PremiumReceivedEventEvaluator : IEventEvaluator<PremiumReceivedEvent, PolicyFundUnitBalanceView>
    {
        public void Evaluate(PolicyFundUnitBalanceView view, PremiumReceivedEvent @event)
        {
        }
    }
}