using System.Linq;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiumsView;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.FrameworkExtensions.ListExtensions;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.UnallocatedReceivedPremiumsViewEventEvaluators
{
    public class AddPremiumEventEvaluator : IEventEvaluator<AddPremiumEvent, UnallocatedReceivedPremiumsView>
    {
        public void Evaluate(UnallocatedReceivedPremiumsView view, AddPremiumEvent @event)
        {
            view.PendingPartitions.AddAll(@event.PartitionDetails.Select(partition => new UnallocatedPremiumPartition
            {
                FundId = partition.FundId,
                Amount = partition.Amount,
                PortionId = partition.PartitionId,
                PremiumId = @event.PremiumId
            }));
        }
    }
}