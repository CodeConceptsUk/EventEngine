using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiums
{
    public class UnallocatedPremiumPartition
    {
        public string PremiumId { get; set; }

        public Guid PortionId { get; set; }

        public string FundId { get; set; }

        public decimal Amount { get; set; }
    }
}