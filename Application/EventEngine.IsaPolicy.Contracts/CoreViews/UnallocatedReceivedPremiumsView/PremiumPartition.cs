using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiumsView
{
    public class PremiumPartition
    {
        public string PremiumId { get; set; }

        public Guid PortionId { get; set; }

        public string FundId { get; set; }

        public decimal Amount { get; set; }
    }
}