using System.Collections.Generic;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView
{
    public class Premium
    {
        public string PremiumId { get; set; }

        public IList<PremiumPartition> Partitions { get; set; } = new List<PremiumPartition>();

        public decimal Total { get; set; }

        public bool IsAllocated { get; set; }

        public bool IsReceived { get; set; }
    }
}