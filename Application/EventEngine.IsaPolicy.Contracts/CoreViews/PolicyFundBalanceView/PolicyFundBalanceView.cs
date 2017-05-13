using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundBalanceView
{
    public class PolicyFundBalanceView : IView
    {
        public string PolicyNumber { get; set; }

        public IList<FundAllocation> FundAllocations { get; set; } = new List<FundAllocation>();
    }
}