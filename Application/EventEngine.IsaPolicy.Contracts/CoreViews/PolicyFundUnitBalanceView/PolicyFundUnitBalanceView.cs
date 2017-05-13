using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalanceView
{
    public class PolicyFundUnitBalanceView : IView
    {
        public string PolicyNumber { get; set; }

        public IList<FundAllocation> FundAllocations { get; set; } = new List<FundAllocation>();
    }
}