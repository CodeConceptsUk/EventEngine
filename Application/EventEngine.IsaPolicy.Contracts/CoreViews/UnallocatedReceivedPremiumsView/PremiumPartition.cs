using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public class PremiumPartition
    {
        public string PremiumId { get; set; }

        public Guid PortionId { get; set; }

        public string FundId { get; set; }

        public decimal Amount { get; set; }
    }
}