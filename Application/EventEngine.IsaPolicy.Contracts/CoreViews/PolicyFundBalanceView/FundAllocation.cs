using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundBalanceView
{
    public class FundAllocation
    {
        public decimal UnitBalance { get; set; }

        public string FundId { get; set; }

        public Guid PortionId { get; set; }
    }
}