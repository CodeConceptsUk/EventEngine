using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiumsView
{
    public class UnallocatedReceivedPremiumsView : IView
    {
        public string PolicyNumber { get; set; }

        public IList<PremiumPartition> PremiumPartitions { get; set; } = new List<PremiumPartition>();
    }
}