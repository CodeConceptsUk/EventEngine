using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalanceView
{
    public class PolicyFundUnitBalanceView : IView
    {
        public IList<FundAllocationBalance> FundAllocations { get; set; } = new List<FundAllocationBalance>();
    }
}