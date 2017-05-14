using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiums;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.UnallocatedReceivedPremiums
{
    public class UnitsAllocatedEventEvaluator : IEventEvaluator<UnitsAllocatedEvent, UnallocatedReceivedPremiumsView>
    {
        public void Evaluate(UnallocatedReceivedPremiumsView view, UnitsAllocatedEvent @event)
        {
            var partition = view.ReceivedPartitions.Single(p => p.PremiumId == @event.PremiumId && p.PortionId == @event.PortionId);
            view.ReceivedPartitions.Remove(partition);
        }
    }
}