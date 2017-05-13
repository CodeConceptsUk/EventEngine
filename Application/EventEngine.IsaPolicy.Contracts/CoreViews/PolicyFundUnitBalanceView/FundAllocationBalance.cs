using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalanceView
{
    public class FundAllocationBalance
    {
        public decimal UnitBalance { get; set; }

        public string FundId { get; set; }

        public Guid PortionId { get; set; }
    }
}