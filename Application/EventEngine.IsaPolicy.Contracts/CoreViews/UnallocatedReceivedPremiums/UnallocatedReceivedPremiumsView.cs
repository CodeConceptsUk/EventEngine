using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiums
{
    //TODO rename to UnallocatedPremiumPartitionsView?
    public class UnallocatedReceivedPremiumsView : IView
    {
        public IList<UnallocatedPremiumPartition> ReceivedPartitions { get; set; } = new List<UnallocatedPremiumPartition>();

        public IList<UnallocatedPremiumPartition> PendingPartitions { get; set; } = new List<UnallocatedPremiumPartition>();
    }
}