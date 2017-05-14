using System.Collections.Generic;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiums
{
    //TODO rename to UnallocatedPremiumPartitionsView?
    public class UnallocatedReceivedPremiumsView : IsaPolicyView
    {
        public IList<UnallocatedPremiumPartition> ReceivedPartitions { get; set; } = new List<UnallocatedPremiumPartition>();

        public IList<UnallocatedPremiumPartition> PendingPartitions { get; set; } = new List<UnallocatedPremiumPartition>();
    }
}