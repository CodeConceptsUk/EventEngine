using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiums;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreEventEvaluators.UnallocatedReceivedPremiums
{
    public class PremiumReceivedEventEvaluator : IEventEvaluator<PremiumReceivedEvent, UnallocatedReceivedPremiumsView>
    {
        public void Evaluate(UnallocatedReceivedPremiumsView view, PremiumReceivedEvent @event)
        {
            var partitionsAffected = view.PendingPartitions.Where(p => p.PremiumId == @event.PremiumId).ToArray();
            partitionsAffected.ForEach(partitionAffected =>
            {
                view.PendingPartitions.Remove(partitionAffected);
                view.ReceivedPartitions.Add(partitionAffected);
            });
        }
    }
}