using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance
{
    public class PolicyFundAllocationUnitBalanceView : IsaPolicyView
    {
        public IList<FundAllocationUnitBalance> FundAllocations { get; set; } = new List<FundAllocationUnitBalance>();
    }
}