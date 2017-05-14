using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiums;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.UnallocatedReceivedPremiums
{
    public class AppliedFundChargeEventEvaluator : IEventEvaluator<AppliedFundChargeEvent, UnallocatedReceivedPremiumsView>
    {
        public void Evaluate(UnallocatedReceivedPremiumsView view, AppliedFundChargeEvent @event)
        {
        }
    }
}