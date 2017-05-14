using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance
{
    public class PolicyFundUnitBalanceView : IView
    {
        public IList<FundAllocationBalance> FundAllocations { get; set; } = new List<FundAllocationBalance>();
    }
}