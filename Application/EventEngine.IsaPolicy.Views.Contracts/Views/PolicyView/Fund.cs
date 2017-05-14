using System.Collections.Generic;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView
{
    public class Fund
    {
        public string FundId { get; set; }

        public IList<FundAllocation> Allocations { get; set; } = new List<FundAllocation>();

        public decimal TotalUnits { get; set; }

        public decimal TotalShadowUnits { get; set; }
    }
}