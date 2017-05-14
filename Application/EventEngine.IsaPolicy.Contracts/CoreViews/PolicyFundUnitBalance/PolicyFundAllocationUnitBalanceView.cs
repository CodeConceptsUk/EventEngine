using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance
{
    public class PolicyFundAllocationUnitBalanceView : IView
    {
        public IList<FundAllocationUnitBalance> FundAllocations { get; set; } = new List<FundAllocationUnitBalance>();
    }
}