using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public class FundAllocation
    {
        public decimal UnitBalance { get; set; }

        public string FundId { get; set; }

        public Guid PortionId { get; set; }
    }
}