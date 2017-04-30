using System.Collections.Generic;

namespace Policy.Plugin.Isa.Policy.Views.PolicyView.Domain
{
    public class Premium
    {
        public string PremiumId { get; set; }

        public IList<PremiumPartition> Partitions { get; set; } = new List<PremiumPartition>();

        public decimal Total { get; set; }

        public bool IsAllocated { get; set; }
    }
}