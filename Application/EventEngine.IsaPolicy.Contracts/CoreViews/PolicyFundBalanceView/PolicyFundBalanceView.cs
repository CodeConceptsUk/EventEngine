using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public class PolicyFundBalanceView : IView
    {
        public string PolicyNumber { get; set; }

        public IList<FundAllocation> FundAllocations { get; set; } = new List<FundAllocation>();
    }
}