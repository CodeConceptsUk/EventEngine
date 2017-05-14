using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance
{
    public class FundAllocationUnitBalance
    {
        public decimal UnitBalance { get; set; }

        public string FundId { get; set; }

        public Guid PortionId { get; set; }

        public DateTime ChargesUpToDateOn { get; set; }
    }
}