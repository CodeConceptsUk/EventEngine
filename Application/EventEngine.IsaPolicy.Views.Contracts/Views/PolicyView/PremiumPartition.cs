using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView
{
    public class PremiumPartition
    {
        public string FundId { get; set; }

        public decimal Amount { get; set; }

        public Guid PortionId { get; set; }
    }
}